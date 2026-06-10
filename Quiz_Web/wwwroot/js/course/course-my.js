function submitDelete(id) {
  if (!confirm('Bạn chắc muốn xóa khóa học này?')) return;
  const f = document.getElementById('deleteForm');
  f.action = `/courses/delete/${id}`;
  f.submit();
}

$(function () {
  $('#coursesTable').DataTable({
    autoWidth: false,
    order: [[5, 'desc']],
    columnDefs: [
      { targets: [1], visible: false, searchable: false },
      { targets: 0, width: 60 }, // ID col
      { targets: 6,  className: 'text-end text-nowrap' }, // action col width
      {
        targets: -1,
        orderable: false,
        searchable: false,
        className: 'text-end text-nowrap',
        render: function (data, type, row) {
          const id = row[0];
          const slug = row[1];
          const viewUrl = `/courses/${slug}`;
          const editUrl = `/courses/builder?id=${id}`;
          return `
            <a href="${viewUrl}" class="btn btn-sm btn-info text-white btn-action me-1">
                <i class="fa-solid fa-eye"></i>
            </a>
            <a href="${editUrl}" class="btn btn-sm btn-warning text-dark btn-action me-1">
                <i class="fa-solid fa-pen-to-square"></i>
            </a>
            <button type="button" class="btn btn-sm btn-danger btn-action" onclick="submitDelete(${id})">
                <i class="fa-solid fa-trash-can"></i>
            </button>`;
        }
      }
    ],
    language: { url: 'https://cdn.datatables.net/plug-ins/2.1.5/i18n/vi.json' }
  });
});