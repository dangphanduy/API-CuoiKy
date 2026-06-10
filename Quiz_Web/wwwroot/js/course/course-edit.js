// Course Edit page scripts
(function(){
    // Auto-generate slug from title
    document.getElementById('titleInput')?.addEventListener('input', function (e) {
        const title = e.target.value;
        const slug = title
            .toLowerCase()
            .normalize('NFD')
            .replace(/[\u0300-\u036f]/g, '')
            .replace(/?/g, 'd')
            .replace(/[^a-z0-9\s-]/g, '')
            .replace(/\s+/g, '-')
            .replace(/-+/g, '-')
            .replace(/^-|-$/g, '');
        const slugEl = document.getElementById('slugInput');
        if (slugEl) slugEl.value = slug;
    });

    // Currency-aware price behavior
    const priceInput = document.getElementById('priceInput');
    const currencySelect = document.getElementById('currencySelect');
    const priceHelp = document.getElementById('priceHelp');

    function formatPricePreview() {
        if (!priceInput) return;
        const val = Number(priceInput.value || 0);
        const ccy = currencySelect?.value || 'VND';
        const locale = ccy === 'VND' ? 'vi-VN' : 'en-US';
        const nf = new Intl.NumberFormat(locale, { style: 'currency', currency: ccy, maximumFractionDigits: ccy === 'VND' ? 0 : 2 });
        if (priceHelp) priceHelp.textContent = val ? `Xem tr??c: ${nf.format(val)}` : '';
    }

    function applyCurrencyRules() {
        if (!priceInput || !currencySelect) return;
        if (currencySelect.value === 'VND') {
            priceInput.step = '1000';
            priceInput.min = '0';
            if (priceInput.value) {
                const v = Math.max(0, Math.round(Number(priceInput.value) / 1000) * 1000);
                priceInput.value = isFinite(v) ? v : '';
            }
        } else {
            priceInput.step = '0.01';
            priceInput.min = '0';
            if (priceInput.value) {
                const v = Math.max(0, Number(priceInput.value));
                priceInput.value = isFinite(v) ? v.toFixed(2).replace(/\.00$/, '') : '';
            }
        }
        formatPricePreview();
    }

    currencySelect?.addEventListener('change', applyCurrencyRules);
    priceInput?.addEventListener('input', formatPricePreview);
    applyCurrencyRules();

    // File preview for cover image
    document.getElementById('coverFileInput')?.addEventListener('change', (e) => {
        const [file] = e.target.files || [];
        const preview = document.getElementById('imagePreview');
        const img = document.getElementById('previewImg');
        if (!file || !preview || !img) { if (preview) preview.style.display = 'none'; return; }
        const reader = new FileReader();
        reader.onload = (ev) => {
            img.src = ev.target.result;
            preview.style.display = 'block';
        };
        reader.onerror = () => { preview.style.display = 'none'; };
        reader.readAsDataURL(file);
    });

    // CKEditor init
    if (window.ClassicEditor) {
        ClassicEditor
            .create(document.querySelector('#Description'), {
                ckfinder: { uploadUrl: '/upload/ck-editor' },
                image: {
                    toolbar: ['imageStyle:inline','imageStyle:block','imageStyle:side','|','toggleImageCaption','imageTextAlternative','|','resizeImage'],
                    resizeUnit: 'px',
                    resizeOptions: [
                        { name: 'resizeImage:720', value: 720, label: '720px' },
                        { name: 'resizeImage:600', value: 600, label: '600px' }
                    ]
                }
            })
            .catch(console.error);
    }
})();