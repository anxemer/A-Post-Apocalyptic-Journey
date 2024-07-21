using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.InteractorSystem
{
    public interface IInteractable
    {
        public string InteractionPromt {  get; }
        public bool Interact(Interactor interactor);
        public void DestroyChest(Interactor interactor);
    }
}
