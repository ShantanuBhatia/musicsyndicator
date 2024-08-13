import { useState, useEffect } from 'react';

const MobileDetection = () => {
    const [isMobile, setIsMobile] = useState(false);

    useEffect(() => {
        const checkMobile = () => {
            const hasTouch = 'ontouchstart' in window || navigator.maxTouchPoints > 0;
            const hasMobileUserAgent = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);

            // Falback is just to check if the screen is narrow
            const hasSmallScreen = window.innerWidth <= 1024;

            setIsMobile(hasTouch && (hasMobileUserAgent || hasSmallScreen));
        };

        checkMobile();
        window.addEventListener('resize', checkMobile);
        return () => window.removeEventListener('resize', checkMobile);
    }, []);

    return isMobile;
};

export default MobileDetection;