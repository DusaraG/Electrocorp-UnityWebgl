import React, { useEffect } from 'react';
import ambientDroneSound from './ambient_drone_sound.mp3'; // Import the audio file

const Music = () => {
  useEffect(() => {
    const audio = document.getElementById("backgroundMusic");
    audio.play();
  }, []);

  return (
    <div className="App">
      {/* Your existing content */}

      <audio id="backgroundMusic" loop>
        <source src={ambientDroneSound} type="audio/mp3" />
      </audio>
    </div>
  );
}

export default Music;