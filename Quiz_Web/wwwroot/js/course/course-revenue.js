// Course Revenue Analytics - Chart and DataTable
$(document).ready(function() {
    // Khởi tạo DataTable
    $('#revenueTable').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/2.1.5/i18n/vi.json'
        },
        order: [[4, 'desc']],
        pageLength: 10,
        dom: '<"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6"f>>rtip'
    });

    // Lấy dữ liệu doanh thu từ data attribute
    const revenueChartElement = document.getElementById('revenueChart');
    if (!revenueChartElement) {
        return; // Không có biểu đồ, thoát
    }

    const revenueData = JSON.parse(revenueChartElement.dataset.revenue || '[]');
    
    if (revenueData.length === 0) {
        return; // Không có dữ liệu
    }

    // Sắp xếp theo thu nhập giảm dần và lấy top 10
    const topCourses = revenueData
        .sort((a, b) => b.InstructorRevenue - a.InstructorRevenue)
        .slice(0, 10);

    // Chuẩn bị dữ liệu cho biểu đồ
    const labels = topCourses.map(item => {
        const title = item.CourseTitle;
        return title.length > 30 ? title.substring(0, 30) + '...' : title;
    });
    
    const grossRevenue = topCourses.map(item => item.GrossRevenue);
    const instructorRevenue = topCourses.map(item => item.InstructorRevenue);
    const platformFee = topCourses.map(item => item.PlatformFee);

    // Tạo biểu đồ cột
    const ctx = revenueChartElement.getContext('2d');
    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [
                {
                    label: 'Tổng doanh thu',
                    data: grossRevenue,
                    backgroundColor: 'rgba(23, 162, 184, 0.7)',
                    borderColor: 'rgba(23, 162, 184, 1)',
                    borderWidth: 2,
                    borderRadius: 5,
                    order: 1
                },
                {
                    label: 'Thu nhập của bạn (60%)',
                    data: instructorRevenue,
                    backgroundColor: 'rgba(40, 167, 69, 0.7)',
                    borderColor: 'rgba(40, 167, 69, 1)',
                    borderWidth: 2,
                    borderRadius: 5,
                    order: 2
                },
                {
                    label: 'Phí nền tảng (40%)',
                    data: platformFee,
                    backgroundColor: 'rgba(255, 193, 7, 0.7)',
                    borderColor: 'rgba(255, 193, 7, 1)',
                    borderWidth: 2,
                    borderRadius: 5,
                    order: 3
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            interaction: {
                mode: 'index',
                intersect: false
            },
            plugins: {
                legend: {
                    position: 'top',
                    labels: {
                        font: {
                            size: 14,
                            family: "'Inter', sans-serif"
                        },
                        padding: 15,
                        usePointStyle: true,
                        pointStyle: 'circle'
                    }
                },
                tooltip: {
                    backgroundColor: 'rgba(0, 0, 0, 0.8)',
                    titleFont: {
                        size: 14,
                        weight: 'bold'
                    },
                    bodyFont: {
                        size: 13
                    },
                    padding: 12,
                    cornerRadius: 8,
                    displayColors: true,
                    callbacks: {
                        label: function(context) {
                            let label = context.dataset.label || '';
                            if (label) {
                                label += ': ';
                            }
                            label += new Intl.NumberFormat('vi-VN', {
                                style: 'currency',
                                currency: 'VND'
                            }).format(context.parsed.y);
                            return label;
                        }
                    }
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        callback: function(value) {
                            return new Intl.NumberFormat('vi-VN', {
                                style: 'currency',
                                currency: 'VND',
                                notation: 'compact',
                                compactDisplay: 'short'
                            }).format(value);
                        },
                        font: {
                            size: 12
                        }
                    },
                    grid: {
                        color: 'rgba(0, 0, 0, 0.05)'
                    }
                },
                x: {
                    ticks: {
                        font: {
                            size: 11
                        },
                        maxRotation: 45,
                        minRotation: 45
                    },
                    grid: {
                        display: false
                    }
                }
            }
        }
    });
});
