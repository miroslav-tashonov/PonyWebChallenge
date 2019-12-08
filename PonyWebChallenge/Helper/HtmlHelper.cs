using System;
using static PonyWebChallenge.Models.CellStateEnumModel;

namespace PonyWebChallenge.Helper
{
    public static class HtmlHelper
    {
        public static string GenerateTopBorder()
        {
            return "+---";
        }

        public static string GenerateBorder()
        {
            return "|";
        }

        public static string GenerateInterection()
        {
            return "+";
        }

        public static string GenerateBreak()
        {
            return "<br/>";
        }

        public static string GenerateBlankTopBorder()
        {
            return "+&nbsp;&nbsp;&nbsp;";
        }
        public static string GenerateWestBorder(CellState cell)
        {
            string returnString = String.Empty;
            if (cell.HasFlag(CellState.Domokun))
            {
                returnString = "|&nbsp;D&nbsp;";
            }
            else if (cell.HasFlag(CellState.Pony))
            {
                returnString = "|&nbsp;P&nbsp;";
            }
            else if (cell.HasFlag(CellState.Endpoint))
            {
                returnString = "|&nbsp;E&nbsp;";
            }
            else
            {
                returnString = "|&nbsp;&nbsp;&nbsp;";
            }

            return returnString;
        }
        public static string GenerateBlankWestBorder(CellState cell)
        {
            string returnString = String.Empty;
            if (cell.HasFlag(CellState.Domokun))
            {
                returnString = "&nbsp;&nbsp;D&nbsp;";
            }
            else if (cell.HasFlag(CellState.Pony))
            {
                returnString = "&nbsp;&nbsp;P&nbsp;";
            }
            else if (cell.HasFlag(CellState.Endpoint))
            {
                returnString = "&nbsp;&nbsp;E&nbsp;";
            }
            else
            {
                returnString = "&nbsp;&nbsp;&nbsp;&nbsp;";
            }

            return returnString;
        }
    }
}