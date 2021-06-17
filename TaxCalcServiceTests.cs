using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMC_DI_TaxJarCalculation;
using System;
using System.Collections.Generic;
using System.Text;
using IMC_DI_TaxJarCalculation.Entities;

namespace IMC_DI_TaxJarCalculation.Tests
{
    [TestClass()]
    public class TaxCalcServiceTests
    {
        TaxCalcService tcs = null;

        [TestInitialize()]
        public void TestInit()
        {
            tcs = new TaxCalcService("https://api.taxjar.com/v2", "5da2f821eee4035db4771edab942a4cc");
        }

        [TestMethod()]
        public void GetLocationTaxRatesTest()
        {
            //this is a service test
            object rates = tcs.GetLocationTaxRates(new Location { City = "Santa Monica", Zip = "90404", Country = "US" });

            Assert.IsNotNull(rates);

            var firstRate = (LocationTaxRate)rates;

            Assert.IsTrue(firstRate.rate.country == "US");


        }

        [TestMethod()]
        public void GetTaxesForOrderTest()
        {
            OrderTax orderTax = tcs.GetTaxesForOrder(new Order
            {
                from_country = "US",
                from_zip = "07001",
                from_state = "NJ",
                to_country = "US",
                to_zip = "07446",
                to_state = "NJ",
                amount = Convert.ToDecimal(16.5),
                shipping = Convert.ToDecimal(1.5),
                line_items = new List<OrderLineItem>
                    {
                        new OrderLineItem
                        {
                            quantity = 1,
                            unit_price = Convert.ToDecimal(15.0),
                            product_tax_code = "31000"
                        }
                    }
            }
            );

            Assert.IsNotNull(orderTax);

            Assert.IsTrue(orderTax.tax.jurisdictions.country == "US");
        }
    }
}