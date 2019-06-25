using StorageMaster.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.Factories
{
    public class ProductFactory
    {
        public Product CreateProduct(string type, double price)
        {
            Product product = null;

            if (type == "Ram")
            {
                product = new Ram(price);
            }
            else if (type == "HardDrive")
            {
                product = new HardDrive(price);
            }
            else if (type == "Gpu")
            {
                product = new Gpu(price);
            }
            else if (type == "SolidStateDrive")
            {
                product = new SolidStateDrive(price);
            }

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new InvalidOperationException("Invalid product type!");
            }
        }
    }
}
