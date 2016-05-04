﻿using SkyView.Nodes;
using System;
using System.Drawing;

namespace SkyView.Image {

    public delegate Image Filter(int height, int width, Image[] inputImages, NodeProperty[] parameters);

    public static class Filters {

        public static Image LoadImage(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            return new Image(parameters[0].value);
        }

        public static Image AddFilter(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 0);
                    Color colorB = inputImages[1].Getcolor(x, y, 0);

                    int A = (colorA.A + colorB.A > 255) ? 255 : colorA.A + colorB.A;
                    int R = (colorA.R + colorB.R > 255) ? 255 : colorA.R + colorB.R;
                    int G = (colorA.G + colorB.G > 255) ? 255 : colorA.G + colorB.G;
                    int B = (colorA.B + colorB.B > 255) ? 255 : colorA.B + colorB.B;

                    finalImage.data[y * width + x] = Color.FromArgb(A, R, G, B) ;
                }
            return finalImage;
        }

        public static Image SubstractFilter(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 0);
                    Color colorB = inputImages[1].Getcolor(x, y, 0);

                    int A = (colorA.A - colorB.A < 0) ? 255 : colorA.A - colorB.A;
                    int R = (colorA.R - colorB.R < 0) ? 255 : colorA.R - colorB.R;
                    int G = (colorA.G - colorB.G < 0) ? 255 : colorA.G - colorB.G;
                    int B = (colorA.B - colorB.B < 0) ? 255 : colorA.B - colorB.B;

                    finalImage.data[y * width + x] = Color.FromArgb(A, R, G, B);
                }
            return finalImage;
        }

        public static Image MultiplyFilter(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 1);
                    Color colorB = inputImages[1].Getcolor(x, y, 1);

                    int A = colorA.A / 255 * colorB.A;
                    int R = colorA.R / 255 * colorB.R;
                    int G = colorA.G / 255 * colorB.G;
                    int B = colorA.B / 255 * colorB.B;

                    finalImage.data[y * width + x] = Color.FromArgb(A, R, G, B);
                }
            return finalImage;
        }

        public static Image DivideFilter(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 1);
                    Color colorB = inputImages[1].Getcolor(x, y, 1);

                    int A = (colorB.A == 0) ? 255 : colorA.A / colorB.A;
                    int R = (colorB.R == 0) ? 255 : colorA.R / colorB.R;
                    int G = (colorB.G == 0) ? 255 : colorA.G / colorB.G;
                    int B = (colorB.B == 0) ? 255 : colorA.B / colorB.B;

                    finalImage.data[y * width + x] = Color.FromArgb(A, R, G, B);
                }
            return finalImage;
        }

        public static Image InvertFilter(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 0);

                    int A = 255 - colorA.A;
                    int R = 255 - colorA.R;
                    int G = 255 - colorA.G;
                    int B = 255 - colorA.B;

                    finalImage.data[y * width + x] = Color.FromArgb(A, R, G, B);
                }
            return finalImage;
        }

        public static Image ConstantFilter(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);
            int A, R, G, B;
            try {
                A = int.Parse(parameters[0].value);
                R = int.Parse(parameters[1].value);
                G = int.Parse(parameters[2].value);
                B = int.Parse(parameters[3].value);
            }
            catch (Exception e) {
                throw e;
            }
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    finalImage.data[y * width + x] = Color.FromArgb(A, R, G, B);
            return finalImage;
        }

        public static Image GetAlphaChannel(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 1);

                    finalImage.data[y * width + x] = Color.FromArgb(colorA.A, 0, 0, 0);
                }
            return finalImage;
        }

        public static Image GetRedChannel(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 1);

                    finalImage.data[y * width + x] = Color.FromArgb(0, colorA.R, 0, 0);
                }
            return finalImage;
        }

        public static Image GetGreenChannel(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 1);

                    finalImage.data[y * width + x] = Color.FromArgb(0, 0, colorA.G, 0);
                }
            return finalImage;
        }

        public static Image GetBlueChannel(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 1);

                    finalImage.data[y * width + x] = Color.FromArgb(0, 0, 0, colorA.B);
                }
            return finalImage;
        }

        public static Image GrayScaleFilter(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 0);
                    int grey = (int)(.299 * colorA.R + .587 * colorA.G + .114 * colorA.B);

                    finalImage.data[y * width + x] = Color.FromArgb(colorA.A, grey, grey, grey);
                }
            return finalImage;
        }

        public static Image LuminosityFilter(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);

            int brightness;
            try {
                brightness = int.Parse(parameters[0].value);
            }
            catch (Exception e) {
                throw e;
            }
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 0);

                    int A = (colorA.A + brightness > 255) ? 255 : colorA.A + brightness;
                    int R = (colorA.R + brightness > 255) ? 255 : colorA.R + brightness;
                    int G = (colorA.G + brightness > 255) ? 255 : colorA.G + brightness;
                    int B = (colorA.B + brightness > 255) ? 255 : colorA.B + brightness;

                    A = (A < 0) ? 0 : A;
                    R = (R < 0) ? 0 : R;
                    G = (G < 0) ? 0 : G;
                    B = (B < 0) ? 0 : B;

                    finalImage.data[y * width + x] = Color.FromArgb(A, R, G, B);
                }
            return finalImage;
        }

        public static Image ThresholdFilter(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);

            int threshold;
            try {
                threshold = int.Parse(parameters[0].value);
            }
            catch (Exception e) {
                throw e;
            }
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 0);

                    int A = (colorA.A > threshold) ? 255 : 0;
                    int R = (colorA.R > threshold) ? 255 : 0;
                    int G = (colorA.G > threshold) ? 255 : 0;
                    int B = (colorA.B > threshold) ? 255 : 0;

                    finalImage.data[y * width + x] = Color.FromArgb(A, R, G, B);
                }
            return finalImage;
        }

        public static Image ColorSelectionFilter(int height, int width, Image[] inputImages, NodeProperty[] parameters) {
            Image finalImage = new Image(height, width);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++) {
                    Color colorA = inputImages[0].Getcolor(x, y, 0);
                    Color colorB = inputImages[1].Getcolor(x, y, 0);

                    int A = (colorA.A - colorB.A < 0) ? 255 : colorA.A - colorB.A;
                    int R = (colorA.R - colorB.R < 0) ? 255 : colorA.R - colorB.R;
                    int G = (colorA.G - colorB.G < 0) ? 255 : colorA.G - colorB.G;
                    int B = (colorA.B - colorB.B < 0) ? 255 : colorA.B - colorB.B;

                    int dist = (int)Math.Sqrt(A * A + R * R + G * G + B * B);

                    finalImage.data[y * width + x] = Color.FromArgb(255 - dist / 2, 255 - dist / 2, 255 - dist / 2, 255 - dist / 2);
                }
            return finalImage;
        }
    }
}
