// ============================================
// SITE.JS — PowerFit Gym
// ============================================

document.addEventListener('DOMContentLoaded', function () {

    // ---- Navbar scroll effect ----
    const nav = document.getElementById('mainNav');
    if (nav) {
        window.addEventListener('scroll', () => {
            nav.classList.toggle('scrolled', window.scrollY > 50);
        });
    }

    // ---- Mobile nav toggle ----
    const navToggle = document.getElementById('navToggle');
    const navLinks = document.getElementById('navLinks');
    if (navToggle && navLinks) {
        navToggle.addEventListener('click', () => {
            navLinks.classList.toggle('open');
            const icon = navToggle.querySelector('i');
            icon.className = navLinks.classList.contains('open') ? 'fas fa-times' : 'fas fa-bars';
        });
    }

    // ---- Dashboard sidebar toggle ----
    const sidebarToggle = document.getElementById('sidebarToggle');
    const sidebar = document.getElementById('sidebar');
    const sidebarOverlay = document.getElementById('sidebarOverlay');

    function openSidebar() {
        sidebar?.classList.add('open');
        sidebarOverlay?.classList.add('open');
        document.body.style.overflow = 'hidden';
    }
    function closeSidebar() {
        sidebar?.classList.remove('open');
        sidebarOverlay?.classList.remove('open');
        document.body.style.overflow = '';
    }
    sidebarToggle?.addEventListener('click', () => {
        if (sidebar?.classList.contains('open')) closeSidebar();
        else openSidebar();
    });
    sidebarOverlay?.addEventListener('click', closeSidebar);

    // ---- Schedule Tabs ----
    const tabBtns = document.querySelectorAll('.tab-btn');
    const tabContents = document.querySelectorAll('.tab-content');
    tabBtns.forEach(btn => {
        btn.addEventListener('click', () => {
            const target = btn.dataset.tab;
            tabBtns.forEach(b => b.classList.remove('active'));
            tabContents.forEach(c => c.classList.remove('active'));
            btn.classList.add('active');
            document.getElementById(target)?.classList.add('active');
        });
    });

    // ---- Scroll Reveal ----
    const reveals = document.querySelectorAll('.reveal');
    const revealObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
                revealObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.12 });
    reveals.forEach(el => revealObserver.observe(el));

    // ---- Counter Animation ----
    const counters = document.querySelectorAll('.count-up');
    const counterObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                animateCounter(entry.target);
                counterObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.5 });
    counters.forEach(el => counterObserver.observe(el));

    function animateCounter(el) {
        const target = parseInt(el.dataset.target || el.textContent);
        let start = 0;
        const duration = 1600;
        const step = target / (duration / 16);
        const timer = setInterval(() => {
            start += step;
            if (start >= target) { el.textContent = target.toLocaleString(); clearInterval(timer); }
            else { el.textContent = Math.floor(start).toLocaleString(); }
        }, 16);
    }

    // ---- Auto-dismiss alert toasts ----
    const toast = document.getElementById('alertToast');
    if (toast) {
        setTimeout(() => {
            toast.style.opacity = '0';
            toast.style.transform = 'translateY(-10px)';
            toast.style.transition = 'all 0.4s ease';
            setTimeout(() => toast.remove(), 400);
        }, 4000);
    }

    // ---- Confirm Delete ----
    document.querySelectorAll('[data-confirm]').forEach(btn => {
        btn.addEventListener('click', function (e) {
            if (!confirm(this.dataset.confirm || 'Are you sure?')) e.preventDefault();
        });
    });

    // ---- Star Rating Interactive ----
    const starInputs = document.querySelectorAll('.star-rating-widget input');
    const starLabels = document.querySelectorAll('.star-rating-widget label');
    starLabels.forEach((label, index) => {
        label.addEventListener('mouseover', () => {
            starLabels.forEach((l, i) => {
                l.style.color = i <= index ? 'var(--gold)' : 'var(--text-muted)';
            });
        });
        label.addEventListener('mouseleave', () => {
            starLabels.forEach(l => l.style.color = '');
        });
    });

    // ---- Hero particles ----
    const particleContainer = document.querySelector('.hero-particles');
    if (particleContainer) {
        for (let i = 0; i < 20; i++) {
            const span = document.createElement('span');
            span.style.left = Math.random() * 100 + '%';
            span.style.top = Math.random() * 100 + '%';
            span.style.animationDelay = Math.random() * 6 + 's';
            span.style.animationDuration = (3 + Math.random() * 5) + 's';
            span.style.width = span.style.height = (Math.random() * 3 + 1) + 'px';
            particleContainer.appendChild(span);
        }
    }

    // ---- Package card selection highlight ----
    document.querySelectorAll('.pkg-radio').forEach(radio => {
        radio.addEventListener('change', function () {
            document.querySelectorAll('.pkg-label').forEach(l => l.classList.remove('selected'));
            const label = document.querySelector(`label[for="${this.id}"]`);
            if (label) label.classList.add('selected');
        });
    });

    // ---- Image preview for file uploads ----
    document.querySelectorAll('input[type="file"][data-preview]').forEach(input => {
        input.addEventListener('change', function () {
            const preview = document.getElementById(this.dataset.preview);
            if (preview && this.files[0]) {
                const reader = new FileReader();
                reader.onload = e => { preview.src = e.target.result; };
                reader.readAsDataURL(this.files[0]);
            }
        });
    });

    // ---- Smooth anchor scrolling ----
    document.querySelectorAll('a[href^="/#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            const href = this.getAttribute('href');
            if (href.startsWith('/#')) {
                const id = href.substring(2);
                const el = document.getElementById(id);
                if (el) {
                    e.preventDefault();
                    el.scrollIntoView({ behavior: 'smooth', block: 'start' });
                    // Close mobile nav if open
                    navLinks?.classList.remove('open');
                }
            }
        });
    });
});
