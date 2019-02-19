using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartPageControl.Helpers;
using StartPageControl.Models;
using StartPageEditor.Helpers;
using StartPageEditor.Models;

namespace StartPageTests
{
  [TestClass]
  public class StartPageViewModelTests
  {
    private readonly Mock<IDialogService> _dialogService;
    private readonly Mock<IJsonFileRepository<StartPageModel>> _startPageRepository;
    private readonly Mock<IStartPageGenerator> _startPageGenerator;
    private readonly Mock<IFileSystem> _fileSystem;
    public StartPageViewModelTests()
    {
      var startPageModel = new StartPageModel();
      startPageModel.ProjectGroups.Add(new ProjectGroup
      {
        Name = "Fake Project Group 1",
        Index=1,
        Background = "groupBackground",
        Foreground = "groupForeground",
        Projects = new List<Project> {
          new Project{ Name = "Project 1",Index=1,Location=@"c:\fake\fake.sln"},
          new Project{ Name = "Project 2",Index=2,Location=@"c:\fake\fake2.sln"},
        }
      });
      startPageModel.ProjectGroups.Add(new ProjectGroup
      {
        Name = "Fake Project Group 2",
        Background = "color1",
        Foreground = "color2",
        Index = 2,
        Projects = new List<Project> {
          new Project{ Name = "Project 1",Index=1,Location=@"c:\fake\fake.sln"},
          new Project{ Name = "Project 2",Index=2,Location=@"c:\fake\fake2.sln"},
          new Project{ Name = "Project 3",Index=3,Location=@"c:\fake\fake2.sln"},
        }
      });
      _dialogService = new Mock<IDialogService>();
      _fileSystem = new Mock<IFileSystem>();
      _fileSystem.Setup(x => x.FileExists(@"c:\fake\fake.json")).Returns(true);

      _startPageRepository = new Mock<IJsonFileRepository<StartPageModel>>();
      _startPageRepository.Setup(x => x.Load(@"c:\fake\fake.json")).Returns(startPageModel);
      _startPageGenerator = new Mock<IStartPageGenerator>();

    }
    [TestMethod]
    public void LoadFile_Returns_Correct_Items()
    {
      var vm = new StartPageViewModel(_dialogService.Object,_startPageGenerator.Object,_fileSystem.Object,_startPageRepository.Object);
      vm.LoadPage(@"c:\fake\fake.json");
      Assert.AreEqual(vm.ProjectGroups.Count, 2);
      Assert.AreEqual(vm.ProjectGroups.First().Projects.Count, 2);
      Assert.AreEqual(vm.ProjectGroups.First(t => t.Index == 2).Projects.Count, 3);
    }
    [TestMethod]
    public void Modifying_Property_Sets_Dirty()
    {
      var vm = new StartPageViewModel(_dialogService.Object, _startPageGenerator.Object, _fileSystem.Object, _startPageRepository.Object);
      vm.LoadPage(@"c:\fake\fake.json");
      Assert.IsFalse(vm.IsDirty);
      vm.ProjectGroups.First().Name = "a new group";
      Assert.IsTrue(vm.IsDirty);
    }

    [TestMethod]
    public void Move_SelectedItem_Sets_Index()
    {
      var vm = new StartPageViewModel(_dialogService.Object, _startPageGenerator.Object, _fileSystem.Object, _startPageRepository.Object);
      vm.LoadPage(@"c:\fake\fake.json");
      vm.SelectedItem = vm.ProjectGroups.First();
      var group = vm.ProjectGroups.First(t => t.Index == 2);
      Assert.AreEqual(((ProjectGroupViewModel)vm.SelectedItem).Index, 1);
      vm.MoveSelectedTreeItemCommand.Execute(Direction.Down);
      Assert.AreEqual(((ProjectGroupViewModel)vm.SelectedItem).Index, 2);
      Assert.AreEqual(group.Index, 1);
      Assert.IsTrue(vm.IsDirty);
    }

    [TestMethod]
    public void Move_Project_Inserts_To_Correct_Group()
    {
      var vm = new StartPageViewModel(_dialogService.Object, _startPageGenerator.Object, _fileSystem.Object, _startPageRepository.Object);
      vm.LoadPage(@"c:\fake\fake.json");
      var fromGroup = vm.ProjectGroups.First();
      var project = vm.ProjectGroups.First().Projects.First();
      var toGroup = vm.ProjectGroups.First(t => t.Index == 2);
      vm.MoveProject(project, toGroup);
      Assert.AreEqual(toGroup.Projects.Count(), 4);
      Assert.AreEqual(fromGroup.Projects.Count(), 1);
      Assert.IsTrue(vm.IsDirty);
    }
  }
}
