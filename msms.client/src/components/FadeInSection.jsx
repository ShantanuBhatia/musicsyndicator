import './FadeInSection.css';

const FadeInSection = ({ children, delay = '0' }) => {
    return (
        <div
            className="fade-in-section"
            style={{
                animationDelay: `${delay}s`,
                animationFillMode: 'forwards',
            }}
        >
            {children}
        </div>
    );
};

export default FadeInSection;