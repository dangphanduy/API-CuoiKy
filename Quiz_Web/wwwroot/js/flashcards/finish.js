document.addEventListener('DOMContentLoaded', function() {
    createConfetti();
});
function createConfetti() {
    const container = document.getElementById('confettiContainer');
    if (!container) return;
    
    const colors = [
        '#FF6B6B',  // Red
        '#4ECDC4',  // Teal
        '#45B7D1',  // Blue
        '#FFA07A',  // Light Salmon
        '#98D8C8',  // Mint
        '#F7DC6F',  // Yellow
        '#BB8FCE'   // Purple
    ];
    
    const confettiCount = 50;
    
    for (let i = 0; i < confettiCount; i++) {
        createConfettiPiece(container, colors);
    }
}

function createConfettiPiece(container, colors) {
    const confetti = document.createElement('div');
    confetti.className = 'confetti';
    
    // Random properties
    const size = Math.random() * 10 + 5; // 5-15px
    const color = colors[Math.floor(Math.random() * colors.length)];
    const left = Math.random() * 100; // 0-100vw
    const rotation = Math.random() * 360; // 0-360deg
    const duration = Math.random() * 3 + 2; // 2-5s
    const delay = Math.random() * 2; // 0-2s
    const shape = Math.random() > 0.5 ? '50%' : '0'; // Circle or square
    
    confetti.style.cssText = `
        position: fixed;
        width: ${size}px;
        height: ${size}px;
        background: ${color};
        left: ${left}vw;
        top: -10px;
        opacity: ${Math.random() * 0.5 + 0.5};
        transform: rotate(${rotation}deg);
        animation: fall ${duration}s linear infinite;
        animation-delay: ${delay}s;
        border-radius: ${shape};
        z-index: 1000;
        pointer-events: none;
    `;
    
    container.appendChild(confetti);
}

function addConfettiAnimation() {
    const style = document.createElement('style');
    style.textContent = `
        @keyframes fall {
            to {
                transform: translateY(100vh) rotate(720deg);
                opacity: 0;
            }
        }
    `;
    document.head.appendChild(style);
}

addConfettiAnimation();
