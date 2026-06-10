// Create Course page specific scripts
document.addEventListener('DOMContentLoaded', () => {
    // Auto-generate slug from title
    document.getElementById('titleInput')?.addEventListener('input', function (e) {
        const title = e.target.value;
        const slug = title
            .toLowerCase()
            .normalize('NFD')
            .replace(/[\u0300-\u036f]/g, '')
            .replace(/đ/g, 'd')
            .replace(/[^a-z0-9\s-]/g, '')
            .replace(/\s+/g, '-')
            .replace(/-+/g, '-')
            .replace(/^-|-$/g, '');
        const slugEl = document.getElementById('slugInput');
        if (slugEl) slugEl.value = slug;
    });

    // Image preview for file input (optional elements; safe no-op if missing)
    const fileInput = document.querySelector('input[name="coverFile"]');
    const preview = document.getElementById('imagePreview');
    const img = document.getElementById('previewImg');
    if (fileInput && preview && img) {
        fileInput.addEventListener('change', (e) => {
            const [file] = e.target.files || [];
            if (!file) { preview.style.display = 'none'; return; }
            const reader = new FileReader();
            reader.onload = (ev) => { img.src = ev.target.result; preview.style.display = 'block'; };
            reader.onerror = () => { preview.style.display = 'none'; };
            reader.readAsDataURL(file);
        });
    }

    // Form validation (basic)
    document.getElementById('courseForm')?.addEventListener('submit', function (e) {
        const title = document.getElementById('titleInput')?.value.trim();
        const slug = document.getElementById('slugInput')?.value.trim();
        if (!title || !slug) {
            e.preventDefault();
            alert('Vui lòng điền đầy đủ tiêu đề và slug khóa học!');
        }
    });

    // Currency-aware price behavior
    const priceInput = document.getElementById('priceInput');
    const currencySelect = document.getElementById('currencySelect');

    function applyCurrencyRules() {
        if (!priceInput || !currencySelect) return;
        if (currencySelect.value === 'VND') {
            priceInput.step = '1000';
            priceInput.min = '0';
            if (priceInput.value) {
                const v = Math.max(0, Math.round(Number(priceInput.value) / 1000) * 1000);
                priceInput.value = Number.isFinite(v) ? v : '';
            }
        } else {
            priceInput.step = '0.01';
            priceInput.min = '0';
            if (priceInput.value) {
                const v = Math.max(0, Number(priceInput.value));
                priceInput.value = Number.isFinite(v) ? v.toFixed(2).replace(/\.00$/, '') : '';
            }
        }
    }
    currencySelect?.addEventListener('change', applyCurrencyRules);
    applyCurrencyRules();

    // CKEditor init
    if (window.ClassicEditor) {
        ClassicEditor
            .create(document.querySelector('#Description'), {
                ckfinder: { uploadUrl: '/upload/ck-editor' }
            })
            .catch(console.error);
    }
});