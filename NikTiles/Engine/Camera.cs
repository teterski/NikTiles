﻿using Microsoft.Xna.Framework;

namespace NikTiles.Engine {

    public static class Camera {

        #region Declarations
        public static Vector2 centre = new Vector2(0, 0);
        public static Vector2 zoom = new Vector2(1, 1);
        public static Matrix transform = Matrix.CreateScale(new Vector3(zoom.X, zoom.Y, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        
        #endregion

        public static void SetCenter(int x, int y) {centre.X = x; centre.Y = y;}

        public static int GetX() {return (int)(centre.X / (Tile.Width() * zoom.X));}

        public static int GetY() {return (int)(centre.Y / (Tile.Height() * zoom.Y));}

        public static int GetPixelsX() {return (int)centre.X;}

        public static int GetPixelsY() {return (int)centre.Y;}

        /// <summary>
        /// Transaltes or moves the camera.
        /// </summary>
        /// <param name="y">Change in Y.</param>
        /// <param name="x">Change in X.</param>
        public static void Translate(int x, int y) {
            centre.X += x * Tile.Width() * zoom.X;
            centre.Y += y * Tile.Height() * zoom.Y;
        }

        /// <summary>
        /// Sets the zoom factor to a specified value.
        /// Can be used for streatching the scene.
        /// </summary>
        /// <param name="x">X zoom amount.</param>
        /// <param name="y">Z zoom amount.</param>
        public static void Zoom(float x, float y) {
            zoom.X += x; zoom.Y += y;
        }

        /// <summary>
        /// Increases/decreases the zoom facotr by th given ammount.
        /// </summary>
        public static void Zoom(float amount) {
            zoom.X += amount; zoom.Y += amount;
            if (zoom.X < 0.1) zoom.X = 0.1f;
            else if (zoom.X > 2.5) zoom.X = 2.5f;
            if (zoom.Y < 0.1) zoom.Y = 0.1f;
            else if (zoom.Y > 2.5) zoom.Y = 2.5f;
        }

        /// <summary>
        /// Updates the transform matrix.
        /// </summary>
        public static void Update() {
            transform = Matrix.CreateScale(new Vector3(zoom.X, zoom.Y, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }

    }
}