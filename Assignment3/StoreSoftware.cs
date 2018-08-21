using System;
using System.IO;
using System.Reflection;
using BinaryTrees;

namespace Assignment3
{
    public class StoreSoftware
    {
        private CustomSoftwareTree<Software> _softwareTree;

        public StoreSoftware()
        {
            _softwareTree = new CustomSoftwareTree<Software>();

            openFile();

            string inputCommand = Console.ReadLine();
            while (inputCommand.Equals("EXIT"))
            {

            }

        }

        private void openFile()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Software.txt");
            string[] tempfile = null;
            try
            {
                tempfile = File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                throw new Exception("Software not Found", e);
            }

            foreach (string line in tempfile)
            {
                string[] softwareinfo = line.Split(',');
                int i = 0;
                var tempsoftware = new Software();
                foreach (string element in softwareinfo)
                {
                    switch (i)
                    {
                        case 0:
                            tempsoftware.Name = element;
                            break;
                        case 1:
                            tempsoftware.Version = element;
                            break;
                        case 2:
                            tempsoftware.Quantity = Convert.ToInt32(element);
                            break;
                        case 3:
                            tempsoftware.Price = Convert.ToInt32(element);
                            break;
                    }
                    i++;
                }
                _softwareTree.AddLeaf(tempsoftware);
                i = 0;
            }

            Console.WriteLine("Software Succesfully Retrieved");
        }

    }

    public class CustomSoftwareTree<T> : IntTree<Software>
    {
        public override void AddLeaf(Software data)
        {
            if (RootLeaf == null)
            {
                RootLeaf = new Leaf<Software>(data);
                RootData = RootLeaf.Data;
            }
            else
            {
                RecursiveAddLeaf(data, RootLeaf);
            }
            Count++;

        }

        private void RecursiveAddLeaf(Software data, Leaf<Software> leaf)
        {
            if (leaf == null) return;
            if (leaf.Data.Name == data.Name) leaf.Data.Quantity++;

            if (data.Name.CompareTo(leaf.Data.Name) < 0)
            {
                if (leaf.Left == null) leaf.Left = new Leaf<Software>(data);
                else RecursiveAddLeaf(data, leaf.Left);
            }

            else if (data.Name.CompareTo(leaf.Data.Name) > 0)
            {
                if (leaf.Right == null) leaf.Right = new Leaf<Software>(data);
                else RecursiveAddLeaf(data, leaf.Right);
            }

        }



    }
}
