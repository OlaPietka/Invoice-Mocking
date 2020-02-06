using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace LabInvoice.Tests
{
    [TestFixture]
    class InvoiceRepository_RETURN
    {
        [Test]
        public void Should_returns_false_when_not_find()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            mockRepository
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => null);

            var sut = new Client(mockRepository.Object);

            Assert.That(sut.Return(new Invoice()), Is.False);
        }

        [Test]
        public void Should_returns_true_when_find()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            mockRepository
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => new Invoice());

            var sut = new Client(mockRepository.Object);

            Assert.That(sut.Return(new Invoice()), Is.True);
        }

        [Test]
        public void Delete_should_be_called_when_find()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            mockRepository
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => new Invoice());

            var sut = new Client(mockRepository.Object);

            sut.Return(new Invoice());

            mockRepository.Verify(x => x.Delete(It.IsAny<Invoice>()), Times.Once);
        }
    }



    class InvoiceRepository_BUY
    {
        [Test]
        public void Add_should_be_called()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            var items = new List<InvoiceItem>
            {
                new InvoiceItem { Id = 0, Name = "item1", Price = 100, Tax = 0.10 },
                new InvoiceItem { Id = 1, Name = "item2", Price = 200, Tax = 0.20 }
            };

            var sut = new Client(mockRepository.Object);

            sut.Buy("Janek", items);

            mockRepository.Verify(x => x.Add(It.IsAny<Invoice>()), Times.Once);
        }

        [Test]
        public void Should_throw_exception()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            var sut = new Client(mockRepository.Object);

            Assert.That(
                () => sut.Buy("", new List<InvoiceItem>()),
                Throws.ArgumentException);
        }
    }



    class InvoiceRepository_UPDATE
    {
        [Test]
        public void Should_returns_false_when_not_find()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            mockRepository
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => null);

            var sut = new Client(mockRepository.Object);

            Assert.That(sut.Update(new Invoice()), Is.False);
        }

        [Test]
        public void Should_returns_true_when_find()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            mockRepository
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => new Invoice());

            var sut = new Client(mockRepository.Object);

            Assert.That(sut.Return(new Invoice()), Is.True);
        }

        [Test]
        public void Update_should_be_calld_when_find()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            mockRepository
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => new Invoice());

            var sut = new Client(mockRepository.Object);

            sut.Update(new Invoice());

            mockRepository.Verify(x => x.Update(It.IsAny<Invoice>()));
        }
    }



    class InvoiceRepository_INVOICEPRICE
    {
        [Test]
        public void InvoicePrice_should_be_calld_when_find()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            mockRepository
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => new Invoice());

            var sut = new Client(mockRepository.Object);

            sut.InvoicePrice(new Invoice());

            mockRepository.Verify(x => x.InvoicePrice(It.IsAny<Invoice>()), Times.Once);
        }

        [Test]
        public void Should_throw_exception_when_not_find()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            mockRepository
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => null);

            var sut = new Client(mockRepository.Object);

            Assert.That(() => sut.InvoicePrice(new Invoice()), Throws.ArgumentNullException);
        }
    }



    class InvoiceRepository_TOTALPRICE
    {
        [Test]
        public void Should_get_totalPrice()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            var sut = new Client(mockRepository.Object);

            sut.showTotalPrice();

            mockRepository.VerifyGet(x => x.totalPrice);
        }
    }



    class InvoiceRepository_CHANGENAME
    {
        [Test]
        public void Should_set_name_when_find()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            mockRepository
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => new Invoice());

            var sut = new Client(mockRepository.Object);

            sut.ChangeName(It.IsAny<int>(), It.IsAny<string>());

            mockRepository.VerifySet(x => x.name = It.IsAny<string>(), Times.Once);
        }

        [Test]
        public void Should_not_set_name_when_not_find()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();

            mockRepository
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => null);

            var sut = new Client(mockRepository.Object);

            sut.ChangeName(It.IsAny<int>(), It.IsAny<string>());

            mockRepository.VerifySet(x => x.name = It.IsAny<string>(), Times.Never);
        }
    }



    class ServiceRepository_CREATEINVOICE
    {
        [Test]
        public void Add_should_calld_when_client_and_invoice_find()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            var mockService = new Mock<IService<Client>>();

            var items = new List<InvoiceItem>
            {
                new InvoiceItem { Id = 0, Name = "item1", Price = 100, Tax = 0.10 },
                new InvoiceItem { Id = 1, Name = "item2", Price = 200, Tax = 0.20 }
            };
            var invoice = new Invoice() { Items = items };
            var client = new Client(mockRepository.Object) { name = "Janek" };

            mockRepository
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => new Invoice());
            mockService
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => new Client(mockRepository.Object));

            var sut = new Service(mockService.Object);

            sut.CreateInvoiceForClient(client, invoice);

            mockRepository.Verify(x => x.Add(It.IsAny<Invoice>()));
        }

        [Test]
        public void Add_should_not_be_calld_when_client_not_find()
        {
            var mockRepository = new Mock<IRepository<Invoice>>();
            var mockService = new Mock<IService<Client>>();

            mockService
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => null);

            var sut = new Service(mockService.Object);

            sut.CreateInvoiceForClient(
                new Client(mockRepository.Object),
                new Invoice());

            mockRepository.Verify(x => x.Add(It.IsAny<Invoice>()), Times.Never);
        }
    }



    class ServiceRepository_CREATECLIENT
    {
        [Test]
        public void GetNextId_should_be_calld()
        {
            var mockService = new Mock<IService<Client>>();

            var sut = new Service(mockService.Object);

            sut.CreateClient(It.IsAny<string>());

            mockService.Verify(x => x.GetNextID(), Times.AtLeastOnce);
        }
    }

    class ServiceRepository_DELETECLIENT
    {
        [Test]
        public void Delete_should_be_calld_when_find()
        {
            var mockService = new Mock<IService<Client>>();
            var mockRepository = new Mock<IRepository<Invoice>>();

            var iv = new List<Invoice>
            {
                new Invoice
                {
                    Id = 0,
                    Customer = "Janek",
                    Items =  new List<InvoiceItem>{ }
                }
            };

            mockRepository.Setup(x => x.getAll()).Returns(() => iv);
            mockService
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => new Client(mockRepository.Object));

            var sut = new Service(mockService.Object);

            sut.DeleteClient(new Client(mockRepository.Object));

            mockService.Verify(x => x.DeleteClient(It.IsAny<Client>()));

        }

        [Test]
        public void Delete_should_not_be_calld_when_not_find()
        {
            var mockService = new Mock<IService<Client>>();
            var mockRepository = new Mock<IRepository<Invoice>>();

            mockService
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => null);

            var sut = new Service(mockService.Object);

            sut.DeleteClient(new Client(mockRepository.Object));

            mockService.Verify(x => x.DeleteClient(It.IsAny<Client>()), Times.Never);
        }

        [Test]
        public void Delete_should_be_calld_exactly()
        {
            var mockService = new Mock<IService<Client>>();
            var mockRepository = new Mock<IRepository<Invoice>>();

            var iv = new List<Invoice>
            {
                new Invoice
                {
                    Id = 0,
                    Customer = "Janek",
                    Items =  new List<InvoiceItem>
                            {
                                new InvoiceItem { Id = 0, Name = "item1", Price = 100, Tax = 0.10 },
                                new InvoiceItem { Id = 1, Name = "item2", Price = 200, Tax = 0.20 }
                            }
                },
                new Invoice
                {
                    Id = 1,
                    Customer = "Janek",
                    Items =  new List<InvoiceItem>
                            {
                                new InvoiceItem { Id = 0, Name = "item3", Price = 240, Tax = 0.06 },
                                new InvoiceItem { Id = 1, Name = "item4", Price = 110, Tax = 0.10 }
                            }
                }
            };

            mockRepository.Setup(x => x.getAll()).Returns(() => iv);
            mockRepository
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => new Invoice());
            mockService
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => new Client(mockRepository.Object));

            var sut = new Service(mockService.Object);

            sut.DeleteClient(new Client(mockRepository.Object));

            mockRepository.Verify(x => x.Delete(It.IsAny<Invoice>()), Times.Exactly(iv.Count));
        }

        [Test]
        public void Delete_should_not_be_calld_when_invoiceRepo_is_empty()
        {
            var mockService = new Mock<IService<Client>>();
            var mockRepository = new Mock<IRepository<Invoice>>();

            mockRepository.Setup(x => x.getAll()).Returns(() =>  null);
            mockService
                .Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(() => new Client(mockRepository.Object));

            var sut = new Service(mockService.Object);

            sut.DeleteClient(new Client(mockRepository.Object));

            mockRepository.Verify(x => x.Delete(It.IsAny<Invoice>()), Times.Never);
        }
    }
}
