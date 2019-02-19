using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartPageEditor.Helpers;

namespace StartPageTests
{
  [TestClass]
  public class FileFinderTests
  {
    private readonly Mock<IFileSystem> _fileSystem;
    private const string FakeDir = @"c:\fake";

    public FileFinderTests()
    {
      _fileSystem = new Mock<IFileSystem>();
      _fileSystem.Setup(x => x.GetDirectoriesAtLevel(new[] { FakeDir }, 0)).Returns(new[] { FakeDir });
      _fileSystem.Setup(x => x.GetSubDirectories(new[] { FakeDir })).Returns(new[] { FakeDir });
      _fileSystem.Setup(x => x.GetDirectoriesRecursive(FakeDir)).Returns(new[] { FakeDir });
      _fileSystem.Setup(x => x.GetFileMatches(FakeDir, "*.sln")).Returns(new[] { $@"{FakeDir}\test.sln", $@"{FakeDir}\test2.sln" }.ToList());

    }
    [TestMethod]
    public void Returns_Correct_Matches()
    {
      var finder = new FileFinder((IFileSystem)_fileSystem.Object);
      var results = finder.Find(new[] { FakeDir }, new[] {@"*.sln"}, 0);

      Assert.AreEqual(results.Count, 1);
      Assert.AreEqual(results.First().Folder, FakeDir);
      Assert.AreEqual(results.First().FileNames.Count, 2);
      Assert.AreEqual(results.First().FileNames.First(), @"c:\fake\test.sln");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Invalid parameters specified.  You must have at least 1 path and 1 pattern")]
    public void Throws_Exception_On_Empty_Parameters()
    {
      var finder = new FileFinder((IFileSystem)_fileSystem.Object);
      finder.Find(new[] { "" }, new[] { @"*.txt" }, 0);
    }

  }
}

