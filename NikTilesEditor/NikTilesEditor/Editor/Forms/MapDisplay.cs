﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using NikTiles.Engine;


namespace NikTiles.Editor.Forms {

    public class MapDisplay : WinFormsGraphicsDevice.GraphicsDeviceControl {

        #region Static
        private static List<Map> maps = new List<Map>(0);
        private static int currentMap;
        public static Map GetCurrentMap() { return maps[currentMap]; }
        public static void SetCurrentMap(int map) { currentMap = map; }

        /// <summary>
        /// Changes teh current map and updates the size of the MapDisplay viewing the map.
        /// </summary>
        /// <param name="map">Map index</param>
        /// <param name="mapDisplay">The control to be resized.</param>
        public static void SetCurrentMap(int map, MapDisplay mapDisplay) {
            currentMap = map;
            mapDisplay.Width = (int)(GetCurrentMap().Width() * Camera.GetZoomX() * Tile.Width() / 2 + Camera.GetZoomX() * Tile.Width() / 2);
            mapDisplay.Height = (int)(GetCurrentMap().Height() * Camera.GetZoomY() * Tile.Height() + Camera.GetZoomY() * Tile.Height() / 2);
        }
        #endregion

        #region Declarations
        private SpriteBatch spriteBatch;
        private int width, height;  //The dimensions of the visible space.
        private Vector2 scroll = new Vector2(0, 0);
        #endregion

        protected override void Initialize() {

            MouseWheel += new MouseEventHandler(mapDisplay_MouseWheel);
            Application.Idle += delegate { Invalidate(); };

            ContentLoader.LoadTextures(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            maps.Add(new Map(100, 100));
            SetCurrentMap(0, this);
            
        }

        protected override void Draw() {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (ContentLoader.floor != null) { //add more advanced check later
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, Camera.GetTransform());
                GetCurrentMap().Draw(spriteBatch, width, height);
                NikTiles.Engine.Cursor.Draw(spriteBatch);
                spriteBatch.End();
            }
        }


        /// <summary>
        /// Udates how much of the MapDsiplay is visible
        /// </summary>
        /// <param name="width">View width</param>
        /// <param name="height">View height</param>
        public void ResizeView(int width, int height) {
            this.width = width;
            this.height = height;
        }


        /// <summary>
        /// MouseEventHandler for MouseWheel. Does not update MapDisplay scrollbars.
        /// Controls Camera zoom.
        /// </summary>
        private void mapDisplay_MouseWheel(object sender, MouseEventArgs e) {
            Camera.Zoom(e.Delta * 0.001f);
            Width = (int)(GetCurrentMap().Width() * Camera.GetZoomX() * Tile.Width() / 2 + Camera.GetZoomX() * Tile.Width() / 2);
            Height = (int)(GetCurrentMap().Height() * Camera.GetZoomY() * Tile.Height() + Camera.GetZoomY() * Tile.Height() / 2);

            //Disables MouseWheel effect on scroll.
            ((HandledMouseEventArgs)e).Handled = true;
        }


    }
}