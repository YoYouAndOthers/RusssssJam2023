using Cysharp.Threading.Tasks;

namespace RussSurvivor.Runtime.Application.SaveLoad
{
  public interface ILoadService
  {
    public void Load();
    public UniTask LoadAsync();
  }
}