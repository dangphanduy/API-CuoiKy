// ? IMPROVED: Common export functions for all reports with UTF-8 BOM and better formatting
function exportToCSV(reportName) {
    const tables = document.querySelectorAll('table');
    let csvContent = reportName + '\n\n';
    
    tables.forEach((table) => {
        const title = table.closest('.card')?.querySelector('.card-header h5')?.textContent?.trim() || 'Data';
        csvContent += title + '\n';
        
        const rows = table.querySelectorAll('tr');
        rows.forEach(row => {
            const cols = row.querySelectorAll('th, td');
            const rowData = Array.from(cols).map(col => {
                // ? IMPROVED: Remove HTML tags (like <span class="badge">) and clean text
                let text = col.textContent.trim();
                
                // ? IMPROVED: Escape quotes and commas for CSV format
                if (text.includes(',') || text.includes('"') || text.includes('\n')) {
                    text = '"' + text.replace(/"/g, '""') + '"';
                }
                
                return text;
            }).join(',');
            
            csvContent += rowData + '\n';
        });
        csvContent += '\n';
    });
    
    // ? IMPROVED: Add UTF-8 BOM to ensure proper encoding in Excel
    const BOM = '\uFEFF';
    const blob = new Blob([BOM + csvContent], { type: 'text/csv;charset=utf-8;' });
    
    const link = document.createElement('a');
    link.href = URL.createObjectURL(blob);
    link.download = reportName.toLowerCase().replace(/\s+/g, '_') + '_' + new Date().toISOString().split('T')[0] + '.csv';
    link.click();
    
    // ? Cleanup
    URL.revokeObjectURL(link.href);
}

function exportToExcel(reportName) {
    const wb = XLSX.utils.book_new();
    const tables = document.querySelectorAll('table');
    
    if (tables.length === 0) {
        alert('Không có d? li?u ?? xu?t!');
        return;
    }
    
    tables.forEach((table, index) => {
        const title = table.closest('.card')?.querySelector('.card-header h5')?.textContent?.trim() || `Sheet${index + 1}`;
        
        // ? IMPROVED: Clone table and remove badges/icons for cleaner export
        const clonedTable = table.cloneNode(true);
        
        // Remove badges and icons
        clonedTable.querySelectorAll('.badge, .bi, i').forEach(el => el.remove());
        
        // ? IMPROVED: Convert table to sheet with proper number formatting
        const ws = XLSX.utils.table_to_sheet(clonedTable, { raw: false });
        
        // ? IMPROVED: Auto-fit column width
        const colWidths = [];
        const range = XLSX.utils.decode_range(ws['!ref']);
        
        for (let C = range.s.c; C <= range.e.c; ++C) {
            let maxWidth = 10;
            for (let R = range.s.r; R <= range.e.r; ++R) {
                const cellAddress = XLSX.utils.encode_cell({ r: R, c: C });
                const cell = ws[cellAddress];
                if (cell && cell.v) {
                    const cellLength = cell.v.toString().length;
                    if (cellLength > maxWidth) {
                        maxWidth = cellLength;
                    }
                }
            }
            colWidths.push({ wch: Math.min(maxWidth + 2, 50) }); // Max 50 chars
        }
        ws['!cols'] = colWidths;
        
        // ? IMPROVED: Sanitize sheet name (max 31 chars, no special chars)
        const sheetName = title.substring(0, 31).replace(/[:\\\/?*\[\]]/g, '');
        XLSX.utils.book_append_sheet(wb, ws, sheetName);
    });
    
    // ? IMPROVED: Export with proper filename
    const filename = reportName.toLowerCase().replace(/\s+/g, '_') + '_' + new Date().toISOString().split('T')[0] + '.xlsx';
    XLSX.writeFile(wb, filename);
}

// ? ADDED: Export single table (useful for specific reports)
function exportTableToExcel(tableId, sheetName, filename) {
    const table = document.getElementById(tableId) || document.querySelector(tableId);
    
    if (!table) {
        alert('Không tìm th?y b?ng d? li?u!');
        return;
    }
    
    const clonedTable = table.cloneNode(true);
    clonedTable.querySelectorAll('.badge, .bi, i').forEach(el => el.remove());
    
    const wb = XLSX.utils.book_new();
    const ws = XLSX.utils.table_to_sheet(clonedTable, { raw: false });
    
    // Auto-fit columns
    const colWidths = [];
    const range = XLSX.utils.decode_range(ws['!ref']);
    for (let C = range.s.c; C <= range.e.c; ++C) {
        let maxWidth = 10;
        for (let R = range.s.r; R <= range.e.r; ++R) {
            const cellAddress = XLSX.utils.encode_cell({ r: R, c: C });
            const cell = ws[cellAddress];
            if (cell && cell.v) {
                const cellLength = cell.v.toString().length;
                if (cellLength > maxWidth) {
                    maxWidth = cellLength;
                }
            }
        }
        colWidths.push({ wch: Math.min(maxWidth + 2, 50) });
    }
    ws['!cols'] = colWidths;
    
    XLSX.utils.book_append_sheet(wb, ws, sheetName.substring(0, 31));
    XLSX.writeFile(wb, filename + '_' + new Date().toISOString().split('T')[0] + '.xlsx');
}