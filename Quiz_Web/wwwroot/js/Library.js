document.addEventListener('DOMContentLoaded', function () {
    const tabs = document.querySelectorAll('#libraryTabs button[data-bs-toggle="tab"]');

    // Load initial content
    loadContent('AllCourses', document.getElementById('all-courses'));

    // Add event listeners to tabs
    tabs.forEach(tab => {
        tab.addEventListener('shown.bs.tab', function (event) {
            const viewName = event.target.getAttribute('data-view');
            const targetId = event.target.getAttribute('data-bs-target');
            const targetPane = document.querySelector(targetId);

            // Load content when tab is clicked
            loadContent(viewName, targetPane);
        });
    });
});

function loadContent(viewName, targetElement) {
    // Show loader
    targetElement.innerHTML = `
        <div class="content-loader">
            <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
    `;

    // Fetch content from server
    fetch(`/Library/${viewName}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.text();
        })
        .then(html => {
            targetElement.innerHTML = html;
        })
        .catch(error => {
            console.error('Error loading content:', error);
            targetElement.innerHTML = `
                <div class="alert alert-danger" role="alert">
                    <i class="fas fa-exclamation-triangle me-2"></i>
                    <strong>Error:</strong> Unable to load content. Please try again later.
                </div>
            `;
        });
}