import React from 'react';
import './BackgroundVideo.css'; // Create this file for styling
import backgroundVideo from './vecteezy_looping-neon-glow-effect-idea-light-bulb-charging-battery_38455056.mp4';

const BackgroundVideo = () => {
  return (
    <div className="background-video">
      <video autoPlay loop muted>
        <source src={backgroundVideo} type="video/mp4" />
      </video>
      {/* Add other content/components on top of the video if needed */}
    </div>
  );
};

export default BackgroundVideo;