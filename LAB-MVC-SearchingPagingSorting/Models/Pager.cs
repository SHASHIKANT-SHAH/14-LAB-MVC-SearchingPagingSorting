using Humanizer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace LAB_MVC_SearchingPagingSorting.Models
{
    public class Pager
    {
        public Pager()
        {
        }

        public string SearchText { get; set; } = "";
        public string Controller { get; set; } = "Product";
        public string Action { get; set; } = "Index";

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int StartRecord { get; private set; }
        public int EndRecord { get; private set; }

        public Pager(int totalItems, int page, int pageSize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;

            StartRecord = (CurrentPage - 1) * PageSize + 1;
            EndRecord = StartRecord - 1 + PageSize;
        }
    }

}

//Here 's a summary of the Pager class:
//•	The class has a default constructor that initializes the properties SearchText, Controller, and Action to empty strings.
//•	The class has several properties that represent the pager's state, such as TotalItems (total number of items), CurrentPage (current page number), PageSize (number of items per page), TotalPages (total number of pages), StartPage (first page number in the pager), EndPage (last page number in the pager), StartRecord (index of the first record on the current page), and EndRecord (index of the last record on the current page).
//•	The class has a parameterized constructor that takes in the totalItems (total number of items), page(current page number), and an optional pageSize (number of items per page, defaulting to 10).
//•	In the constructor, the total number of pages (totalPages) is calculated based on the totalItems and pageSize.
//•	The start and end page numbers are determined based on the current page number (currentPage).
//•	If the start page is less than or equal to 0, it is adjusted to 1 and the end page is recalculated accordingly.
//•	If the end page exceeds the total number of pages, it is adjusted to the last page and the start page is recalculated if necessary.
//•	The constructor assigns the calculated values to the corresponding properties.

//This Pager class can be used to represent and calculate pagination information for displaying data in a paginated manner, such as in a web application where data is displayed in chunks across multiple pages. The properties provide the necessary information for rendering the pager UI and determining the range of records to fetch or display for the current page.