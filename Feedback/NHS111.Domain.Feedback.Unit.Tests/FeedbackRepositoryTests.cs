using NHS111.Domain.Feedback.Convertors;
using NHS111.Domain.Feedback.Repository;
using NHS111.Utils.Configuration;
using NUnit.Framework;

namespace NHS111.Domain.Feedback.Unit.Tests
{
    [TestFixture]
    public class FeedbackRepositoryTests
    {
        [Test]
        public void DetermineOffset_ZeroPagesEmptyPage()
        {
            int pageNumber = 0;
            int pageSize = 0;
            int offset = FeedbackRepository.DetermineOffset(pageNumber, pageSize);
            Assert.AreEqual(0, offset);
        }

        [Test]
        public void DetermineOffset_OneEmptyPage()
        {
            int pageNumber = 1;
            int pageSize = 0;
            int offset = FeedbackRepository.DetermineOffset(pageNumber, pageSize);
            Assert.AreEqual(0, offset);
        }

        [Test]
        public void DetermineOffset_ZeroPagesOneItem()
        {
            int pageNumber = 0;
            int pageSize = 1;
            int offset = FeedbackRepository.DetermineOffset(pageNumber, pageSize);
            Assert.AreEqual(0, offset);
        }

        [Test]
        public void DetermineOffset_OnePageTenItems()
        {
            int pageNumber = 1;
            int pageSize = 10;
            int offset = FeedbackRepository.DetermineOffset(pageNumber, pageSize);
            Assert.AreEqual(0, offset);
        }
        
        [Test]
        public void DetermineOffset_TwoPages()
        {
            int pageNumber = 2;
            int pageSize = 2;
            int offset = FeedbackRepository.DetermineOffset(pageNumber, pageSize);
            Assert.AreEqual(2, offset);
        }

        [Test]
        public void DetermineOffset_TwoPagesTwentyItems()
        {
            int pageNumber = 2;
            int pageSize = 20;
            int offset = FeedbackRepository.DetermineOffset(pageNumber, pageSize);
            Assert.AreEqual(20, offset);
        }

        [Test]
        public void DetermineOffset_FiftyPages_TwentyItems()
        {
            int pageNumber = 50;
            int pageSize = 20;
            int offset = FeedbackRepository.DetermineOffset(pageNumber, pageSize);
            Assert.AreEqual(980, offset);
        }

        [Test]
        public void DetermineOffset_TenPages_OneItem()
        {
            int pageNumber = 10;
            int pageSize = 1;
            int offset = FeedbackRepository.DetermineOffset(pageNumber, pageSize);
            Assert.AreEqual(9, offset);
        }

        [Test]
        public void DetermineOffset_OnePage_OneItem()
        {
            int pageNumber = 1;
            int pageSize = 1;
            int offset = FeedbackRepository.DetermineOffset(pageNumber, pageSize);
            Assert.AreEqual(0, offset);
        }
    }
}
