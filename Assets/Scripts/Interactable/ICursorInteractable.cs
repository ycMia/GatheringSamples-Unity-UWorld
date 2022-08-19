using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScripts.Interactable
{
    public interface ICursorInteractable
    {
        void OnCursorClick();
    }

    public interface ICursorSingleClickable
    {
        void OnSingleClick();
    }
    public interface ICursorDoubleClickable
    {
        void OnDoubleClick();
    }
}
