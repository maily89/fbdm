using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.CommonUtilities
{
    public class Constants
    {
        /************** PAGES NAMES *************/



        /************** END OF PAGES NAMES ***************/



        /************** ERROR MESSAGES ****************/

        public const string ERR_INDEX_FI_FINANCIAL_INDEX = "Error when displaying the list of Financial Indexes";

        public const string ERR_ADD_POST_FI_FINANCIAL_INDEX = "Error when adding new Financial Index. May be due to"
                                                                    + " duplicated key or other SQL Exception";

        public const string ERR_EDIT_FI_FINANCIAL_INDEX = "Error when select the financial index. Please try again";

        public const string ERR_EDIT_POST_FI_FINANCIAL_INDEX = "Error when editting financial index. May be due to"
                                                                    + " duplicated key or other SQL Exception";

        public const string ERR_DELETE_FI_FINANCIAL_INDEX = "Error when deleting the financial index. Try again later";

        /************** END OF ERROR MESSAGES ****************/



        /************** SUCCESSFUL MESSAGES ****************/

        public const string SCC_ADD_FI_FINANCIAL_INDEX = "A new Financial Index has been added successfully";

        public const string SCC_EDIT_POST_FI_FINANCIAL_INDEX_1 = "The financial index with ID ";

        public const string SCC_EDIT_POST_FI_FINANCIAL_INDEX_2 = " has been editted successfully";

        public const string SCC_DELETE_FI_FINANCIAL_INDEX = "A financial index has been deleted successfully";

        /************** END OF SUCCESSFUL MESSAGES ***************/
    }
}
