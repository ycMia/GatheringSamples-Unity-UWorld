using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScripts.Interactable
{
    [Obsolete("\nThis method is currently not supported by The ClickListeners.\nPlease consider using ICursorSingleClickable etc. instead.")]
    public interface ICursorInteractable
    {
        void OnCursorClick();
    }

    public interface ICursorSingleClickable
    {
        void OnSingleClick();
    }
    public interface IR_CursorSingleClickable
    {
        void OnR_SingleClick();
    }
    public interface ICursorDoubleClickable
    {
        void OnDoubleClick();
    }
    public interface IR_CursorDoubleClickable
    {
        void OnR_DoubleClick();
    }
    public interface ICursorHoverable
    {
        void OnCursorHover();
        void OnCursorNotHover();
    }
}
