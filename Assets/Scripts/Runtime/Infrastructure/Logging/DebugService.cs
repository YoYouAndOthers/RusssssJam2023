using QFSW.QC;
using RussSurvivor.Runtime.Infrastructure.Inputs;
using UnityEngine;

namespace RussSurvivor.Runtime.Infrastructure.Logging
{
  public class DebugService : IDebugService
  {
    private readonly IInputService _inputService;
    private QuantumConsole _console;

    public DebugService(IInputService inputService) =>
      _inputService = inputService;

    public void Initialize(QuantumConsole console)
    {
      Debug.Log("Debug service initialized");
      _console = console;
      _inputService.OnConsoleCalled += OnConsoleCalled;
      _console.Deactivate();
    }

    public void Dispose()
    {
      _inputService.OnConsoleCalled -= OnConsoleCalled;
    }

    private void OnConsoleCalled()
    {
      if (_console.IsActive)
        _console.Deactivate();
      else
        _console.Activate();
    }
  }
}