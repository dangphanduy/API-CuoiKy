// Detail page: ensure images inside ck-content don't have inline w/h
window.addEventListener('DOMContentLoaded', () => {
  document.querySelectorAll('.course-content img').forEach(img => {
    img.removeAttribute('width');
    img.removeAttribute('height');
  });
});