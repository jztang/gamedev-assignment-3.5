# gamedev-assignment-3.5

My mod for the grid game is what Othello/Reversi would be like if it was played on a hexagonal grid. At the start, I knew I wanted to do something with a hexagonal grid, but also thought it would harder to implement than a "normal", square grid. Turns out, a hexagonal grid can also be represented by a 2D array. The math to get adjacent tiles is a little more complicated, but it wasn't too bad.

My algorithm for checking for tile flips involves checking each direction when a tile is placed. This checking needs to stop at the border of the grid. I couldn't figure out an elegant way to check for the border because of the nature of a hexagonal grid, so I ended up hardcoding the values, which I wasn't too happy about.

I also wanted to implement animations for flipping tiles. My idea was to have the tiles flip one after the other, like a ripple effect. In addition, I wanted to have an accompanying sound such that for each successive flip, a note of a musical scale would be played, playing more of the scale for longer streaks of tile flips.