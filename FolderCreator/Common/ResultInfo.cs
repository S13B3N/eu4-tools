using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderCreator.Common
{
   public class ResultInfo<T>
   {
      public ResultInfo ()
      {
         ErrorCode = 0;

         Message = "";

         ClassName = "";
         Method    = "";
         File      = "";
         Line      = 0 ;
      }

      //------------------------------------------------------------------------

      public bool IsOK ()
      {
         return ErrorCode == 0;
      }

      public bool IsNotOK ()
      {
         return !IsOK ();
      }

      //------------------------------------------------------------------------

      public void SetResult ( int errorCode, string message, T tag, string file = "", int line = 0, string className = "", string method = "" )
      {
         ErrorCode = errorCode;

         Message = message;

         Tag = tag;

         ClassName = className;
         Method    = method   ;
         File      = file     ;
         Line      = line     ;
      }

      //------------------------------------------------------------------------
      // Properties
      //------------------------------------------------------------------------

      public int    ErrorCode;
      public string Message  ;
      public T      Tag      ;

      public string ClassName;
      public string Method   ;
      public string File     ;
      public int    Line     ;
   }
}
