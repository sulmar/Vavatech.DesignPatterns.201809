using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.Flyweight
{
    // Pozwala zaoszczędzić miejsce w pamięci
    // Dedykowane w przypadkach gdy mamy bardzo dużą ilość obiektów
    class Program
    {
        static void Main(string[] args)
        {
            FlyWeightTest();
        }

        private static void FlyWeightTest()
        {
            Game game = new Game(Factory.Create());
            game.Play();
           
        }
    }


    public class Game
    {
        private IList<TreeConcret> trees { get; set; }

        public Game()
        {
        }

        public Game(IList<TreeConcret> trees)
        {
            this.trees = trees;
        }

        public void Play()
        {
            foreach (var tree in trees)
            {
                tree.Draw();
            }
        }
    }

    public class Factory
    {
        public static IList<TreeConcret> Create()
        {
            TreeModel treeModel1 = new TreeModel(new Mesh(10), new Texture("###"), new Texture("==="));
            TreeModel treeModel2 = new TreeModel(new Mesh(5), new Texture(">>>"), new Texture("<<<"));

            List<TreeConcret> trees = new List<TreeConcret>
            {
                new TreeConcret(treeModel1, new Vector(10, 30), 30, 1, new Color(200, 100, 50), new Color(100, 100, 100)),
                new TreeConcret(treeModel1, new Vector(20, 15), 30, 1, new Color(200, 100, 50), new Color(100, 100, 100)),
                new TreeConcret(treeModel1, new Vector(40, 30), 30, 1, new Color(200, 100, 50), new Color(100, 100, 100)),
                new TreeConcret(treeModel1, new Vector(60, 30), 30, 1, new Color(200, 100, 50), new Color(100, 100, 100)),
                new TreeConcret(treeModel2, new Vector(40, 30), 30, 1, new Color(200, 100, 50), new Color(100, 100, 100)),
                new TreeConcret(treeModel2, new Vector(60, 30), 30, 1, new Color(200, 100, 50), new Color(100, 100, 100)),

            };

            return trees;
        }
    }

    public class Tree
    {
        Mesh mesh_;
        Texture bark_;
        Texture leaves_;
        Vector position_;
        double height_;
        double thickness_;
        Color barkTint_;
        Color leafTint_;
    }

    // rozbijamy na osobne klasy:
    public class TreeModel
    {
        Mesh mesh_;
        Texture bark_;
        Texture leaves_;

        public TreeModel(Mesh mesh_, Texture bark_, Texture leaves_)
        {
            this.mesh_ = mesh_;
            this.bark_ = bark_;
            this.leaves_ = leaves_;
        }

        public void Draw(int x, int y, Color leafColor)
        {
            Console.WriteLine($"Tree: mesh: {mesh_} bark: {bark_} leaves: {leaves_} ({x}:{y}) leafColor={leafColor}");
        }
    }

    public class TreeConcret
    {
        public TreeModel TreeModel { get; set; }

        Vector position_;
        double height_;
        double thickness_;
        Color barkTint_;
        Color leafTint_;

        public TreeConcret(TreeModel treeModel, Vector position_, double height_, double thickness_, Color barkTint_, Color leafTint_)
        {
            TreeModel = treeModel;
            this.position_ = position_;
            this.height_ = height_;
            this.thickness_ = thickness_;
            this.barkTint_ = barkTint_;
            this.leafTint_ = leafTint_;
        }

        public void Draw()
        {
            TreeModel.Draw(position_.X, position_.Y, leafTint_);
        }
    }

    public class Vector
    {
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Mesh
    {
        public Mesh(int size)
        {
            Size = size;
        }

        public int Size { get; set; }

        public override string ToString() => Size.ToString();
    }

    public class Texture
    {
        public Texture(string content)
        {
            Content = content;
        }

        public string Content { get; set; }

        public override string ToString() => Content;
    }

    public struct Color
    {
        public Color(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public override string ToString() => $"(R={Red}, G={Green}, B={Blue})";

        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
    }
}
