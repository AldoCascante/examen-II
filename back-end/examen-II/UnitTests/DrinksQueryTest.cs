using examen_II.Application;
using examen_II.Domain;
using examen_II.Infrastructure;
using Moq;

namespace UnitTests
{
    public class DrinksQueryTest
    {
        private Mock<IDrinksRepository> mockRepo;
        private IDrinksQuery testQuery;
        [SetUp]
        public void Setup()
        {
            mockRepo = new Mock<IDrinksRepository>();
            testQuery = new DrinksQuery(mockRepo.Object);

            List<DrinkInfoDTO> drinksDB = new List<DrinkInfoDTO>();

            drinksDB.Add(new DrinkInfoDTO
            {
                name = "Coca Cola",
                available = 10,
                price = 800,
            });

            drinksDB.Add(new DrinkInfoDTO
            {
                name = "Pepsi",
                available = 8,
                price = 750,
            });

            drinksDB.Add(new DrinkInfoDTO
            {
                name = "Fanta",
                available = 10,
                price = 950,
            });

            drinksDB.Add(new DrinkInfoDTO
            {
                name = "Sprite",
                available = 15,
                price = 975,
            });

            
            mockRepo.Setup(repo => repo.GetAvailableDrinks()).Returns(drinksDB);
        }

        [Test]
        public void ValidateOrderTestHappyPath()
        {
            TransaccionModel order = new TransaccionModel();
            order.drinkOrders = new List<string>();
            order.drinkOrders.Add("Coca Cola");
            order.drinkOrders.Add("2");

            bool expectedOutcome = true;
            bool outcome = testQuery.ValidateOrder(order);

            Assert.That(outcome, Is.EqualTo(expectedOutcome));
        }

        [Test]
        public void ValidateOrderTestOddOrderSize()
        {
            TransaccionModel order = new TransaccionModel();
            order.drinkOrders = new List<string>();
            order.drinkOrders.Add("Coca Cola");
            order.drinkOrders.Add("2");
            order.drinkOrders.Add("Sprite");

            bool expectedOutcome = false;
            bool outcome = testQuery.ValidateOrder(order);

            Assert.That(outcome, Is.EqualTo(expectedOutcome));
        }

        [Test]
        public void ValidateOrderTestInvalidDrink()
        {
            TransaccionModel order = new TransaccionModel();
            order.drinkOrders = new List<string>();
            order.drinkOrders.Add("Coca Cola");
            order.drinkOrders.Add("2");
            order.drinkOrders.Add("Mirinda");
            order.drinkOrders.Add("1");

            bool expectedOutcome = false;
            bool outcome = testQuery.ValidateOrder(order);

            Assert.That(outcome, Is.EqualTo(expectedOutcome));
        }

        [Test]
        public void ValidateOrderTestNegativeQuantity()
        {
            TransaccionModel order = new TransaccionModel();
            order.drinkOrders = new List<string>();
            order.drinkOrders.Add("Coca Cola");
            order.drinkOrders.Add("-2");
            order.drinkOrders.Add("Fanta");
            order.drinkOrders.Add("1");

            bool expectedOutcome = false;
            bool outcome = testQuery.ValidateOrder(order);

            Assert.That(outcome, Is.EqualTo(expectedOutcome));
        }

        [Test]
        public void ValidateOrderTestQuantityZero()
        {
            TransaccionModel order = new TransaccionModel();
            order.drinkOrders = new List<string>();
            order.drinkOrders.Add("Coca Cola");
            order.drinkOrders.Add("3");
            order.drinkOrders.Add("Fanta");
            order.drinkOrders.Add("0");

            bool expectedOutcome = false;
            bool outcome = testQuery.ValidateOrder(order);

            Assert.That(outcome, Is.EqualTo(expectedOutcome));
        }

        [Test]
        public void ValidateOrderTestNullList()
        {
            TransaccionModel order = new TransaccionModel();
            order.drinkOrders = new List<string>();
            order.drinkOrders.Add(null);
            order.drinkOrders.Add(null);
            order.drinkOrders.Add(null);
            order.drinkOrders.Add(null);

            bool expectedOutcome = false;
            bool outcome = testQuery.ValidateOrder(order);

            Assert.That(outcome, Is.EqualTo(expectedOutcome));
        }

        [Test]
        public void CheckAvailabilityTestHappyPath()
        {
            TransaccionModel order = new TransaccionModel();
            order.drinkOrders = new List<string>();
            order.drinkOrders = new List<string>();
            order.drinkOrders.Add("Coca Cola");
            order.drinkOrders.Add("3");
            order.drinkOrders.Add("Fanta");
            order.drinkOrders.Add("1");

            bool expectedOutcome = true;
            bool outcome = testQuery.CheckAvailability(order);

            Assert.That(outcome, Is.EqualTo(expectedOutcome));
        }

        [Test]
        public void CheckAvailabilityTestUnavailable()
        {
            TransaccionModel order = new TransaccionModel();
            order.drinkOrders = new List<string>();
            order.drinkOrders = new List<string>();
            order.drinkOrders.Add("Coca Cola");
            order.drinkOrders.Add("3000");

            bool expectedOutcome = false;
            bool outcome = testQuery.CheckAvailability(order);

            Assert.That(outcome, Is.EqualTo(expectedOutcome));
        }

    }
}