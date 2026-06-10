(function () {
	"use strict";

	var treeviewMenu = $('.app-menu');

	// Toggle Sidebar
	$('[data-toggle="sidebar"]').click(function (event) {
		event.preventDefault();
		$('.app').toggleClass('sidenav-toggled');
	});

	// Activate sidebar treeview toggle
	$("[data-toggle='treeview']").click(function (event) {
		event.preventDefault();
		$(this).parent().toggleClass('is-expanded');
	});

	// Set active menu item based on current URL
	var currentPath = window.location.pathname.toLowerCase();
	$('.app-menu a').each(function() {
		var href = $(this).attr('href');
		if (href) {
			var linkPath = href.toLowerCase();
			// Exact match for better accuracy
			if (currentPath === linkPath || (currentPath.endsWith(linkPath.split('/').pop()) && linkPath !== '/')) {
				$(this).addClass('active');
				if ($(this).hasClass('treeview-item')) {
					$(this).closest('.treeview').addClass('is-expanded');
				}
			}
		}
	});

})();
