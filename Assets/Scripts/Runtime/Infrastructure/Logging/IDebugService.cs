using System;
using QFSW.QC;

namespace RussSurvivor.Runtime.Infrastructure.Logging
{
  public interface IDebugService : IDisposable
  {
    void Initialize(QuantumConsole console);
  }
}