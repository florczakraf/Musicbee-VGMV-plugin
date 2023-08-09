using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicBeePlugin {
    class bouncingImage {
        public int x = 0;
        public int y = 0;
        public float angle = 0;
        int velX = 0;
        int velY = 0;
        int grav = -1;
        public int ID = 0;
        //bounds 0-500

        public bouncingImage() {
            Random rand = new Random();
            x = rand.Next(0, 400);
            if(x > 200) {
                x = 500;
                velX = rand.Next(-11, -1) - 7;
            }
            else {
                x = -100;
                velX = rand.Next(1, 11) + 7;
            }
            y = rand.Next(0, 201);

            velY = rand.Next(-5, 10);

            angle = rand.Next(0, 360);
            ID = rand.Next(0, 2);
        }

        public void tick(float dt) {
            x += (int) (velX * dt/100);
            y += (int) (velY * dt/100);
            velY -= grav;
            angle += velX;
        }

    }
}
