using System;
using System.Collections.Generic;
using System.Text;

namespace BagerLib.model
{
    public class Kage
    {
        private string _name;
        private double _price;
        private int _noOfPieces;

        // HUSK default kostruktør
        public Kage()
        {
        }

        public Kage(string name, double price, int noOfPieces)
        {
            _name = name;
            _price = price;
            _noOfPieces = noOfPieces;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public double Price
        {
            get => _price;
            set => _price = value;
        }

        public int NoOfPieces
        {
            get => _noOfPieces;
            set => _noOfPieces = value;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Price)}: {Price}, {nameof(NoOfPieces)}: {NoOfPieces}";
        }
    }
}
