using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsMaster.Business;
using WordsMaster.Business.Managers;
using WordsMaster.Domain.Contracts.Managers;
using WordsMaster.Domain.Contracts.Repositories;
using WordsMaster.Domain.Contracts.Verifiers;
using WordsMaster.Domain.Entities;
using Xunit;

namespace WordsMaster.Business.UnitTests
{
    public class WordsManagerTests
    {
        private WordsManager Instance;
        private Mock<IArgumentVerifier> _mock;        
        private Mock<ISubwordsProcessor> _subwordsProcessorMock;
        private Mock<IWordsLoader> _wordsLoaderMock;

        public WordsManagerTests()
        {
            _mock = new Mock<IArgumentVerifier>();
            _wordsLoaderMock = new Mock<IWordsLoader>();
            _subwordsProcessorMock = new Mock<ISubwordsProcessor>();

            Instance = new WordsManager(_mock.Object, _subwordsProcessorMock.Object);
        }


        [Fact]
        public void Process_ArgumentsFailVerification_DoesNotLoadWords()
        {
            // Arrange            
            _mock.Setup(o => o.Verify(SomeArguments)).Returns(NegativeResult);

            // Act
            Instance.Process(SomeArguments);

            // Assert
            _wordsLoaderMock.Verify(o => o.LoadAllWords(), Times.Never());
        }


        [Fact]
        public void Process_ArgumentsPassVerification_LoadsWords()
        {
            // Arrange            
            SetVerificationAndLoadWordsToPass();

            // Act
            Instance.Process(SomeArguments);

            // Assert
            _wordsLoaderMock.Verify(o => o.LoadAllWords(), Times.Once());
        }


        private void SetVerificationToPassButLoadWordsToFail()
        {
            _mock.Setup(o => o.Verify(SomeArguments)).Returns(PositiveResult);
            _wordsLoaderMock.Setup(o => o.LoadAllWords()).Returns(NegativeLoadResult);
        }


        public void SetVerificationAndLoadWordsToPass()
        {
            _mock.Setup(o => o.Verify(SomeArguments)).Returns(PositiveResult);
            _wordsLoaderMock.Setup(o => o.LoadAllWords()).Returns(PositiveLoadResult);
        }


        public string[] SomeArguments
        {
            get { return new string[] { "lad" }; }
        }


        public VerificationResult PositiveResult
        {
            get
            {
                return new VerificationResult
                {
                    Passed = true,
                    Message = string.Empty
                };
            }
        }


        public VerificationResult NegativeResult
        {
            get
            {
                return new VerificationResult
                {
                    Passed = false,
                    Message = "I failed for the test!"
                };
            }
        }


        public Result PositiveLoadResult
        {
            get
            {
                return new Result { Passed = true };
            }
        }

        public Result NegativeLoadResult
        {
            get { return new Result { Passed = false, Message = "I fail for my test!" }; }
        }
    }
}


