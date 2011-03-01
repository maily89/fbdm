﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FBD.CommonUtilities
{
    public class Constants
    {
        /************** PAGES NAMES *************/



        /************** END OF PAGES NAMES ***************/

        /*****************CLASS NAMES *******************/
        //BUSINESS
        public const string BUSINESS_INDUSTRY = "Business Industry";
        public const string BUSINESS_RANKING = "Business Ranking";
        public const string BUSINESS_RANKING_STRUCTURE = "Business Ranking Structure";


        /******************END OF CLASS NAMES

        /************** ERROR MESSAGES ****************/

        //ERROR
        public const string ERR_INDEX = "Error when displaying the list of {0}";
        public const string ERR_ADD_POST = "Error when adding new {0}. Please recheck your input ";
        public const string ERR_EDIT = "Error when select the {0}. Please try again";
        public const string ERR_EDIT_POST = "Error when editting {0}. Please recheck your input ";
        public const string ERR_DELETE = "Error when deleting the {0}. Try again later";
        public const string ERR_UPDATE_FIPROPORTION = "Error when updating to the financial index {0}. Try again later";
        public const string ERR_DISPLAY_FIPROPORTION = "Error when displaying the selected business industry";
        public const string ERR_POST_FIPROPORTION = "Error when posting information to server. Try again later";
        
        // FINANCIAL INDEX

        public const string ERR_INDEX_FI_FINANCIAL_INDEX = "Error when displaying the list of Financial Indexes";
        
        public const string ERR_ADD_POST_FI_FINANCIAL_INDEX = "Error when adding new Financial Index. Plz check your "
                                                                            + "input carefully!";

        public const string ERR_EDIT_FI_FINANCIAL_INDEX = "Error when select the financial index. Please try again";

        public const string ERR_EDIT_POST_FI_FINANCIAL_INDEX = "Error when editting financial index. Plz check your "
                                                                            + "input carefully!";

        public const string ERR_DELETE_FI_FINANCIAL_INDEX = "Error when deleting the financial index. Try again later";


        // FINANCIAL INDEX LEVELS

        public const string ERR_INDEX_FI_FINANCIAL_INDEX_LEVEL = "Error when displaying the list of Financial Index Levels";

        public const string ERR_ADD_POST_FI_FINANCIAL_INDEX_LEVEL = "Error when adding new Financial Index Level. Plz check your "
                                                                            + "input carefully!";

        public const string ERR_EDIT_FI_FINANCIAL_INDEX_LEVEL = "Error when select the financial index level. Please try again";

        public const string ERR_EDIT_POST_FI_FINANCIAL_INDEX_LEVEL = "Error when editting financial index level. Plz check your "
                                                                            + "input carefully!";

        public const string ERR_DELETE_FI_FINANCIAL_INDEX_LEVEL = "Error when deleting the financial index level. Try again later";

        

        //SYSTEM USER GROUPS
        public const string ERR_INDEX_SYS_USER_GROUPS = "ERR_INDEX_SYS_USER_GROUPS";
        public const string ERR_ADD_POST_SYS_USER_GROUPS = "ERR_ADD_POST_SYS_USER_GROUPS";
        public const string ERR_EDIT_SYS_USER_GROUPS = "ERR_EDIT_SYS_USER_GROUPS";
        public const string ERR_EDIT_POST_SYS_USER_GROUPS = "ERR_EDIT_POST_SYS_USER_GROUPS";
        public const string ERR_DELETE_SYS_USER_GROUPS = "ERR_DELETE_SYS_USER_GROUPS";
        
        //SYSTEM BRANCHES
        public const string ERR_INDEX_SYS_BRANCHES = "ERR_INDEX_SYS_BRANCHES";
        public const string ERR_ADD_POST_SYS_BRANCHES = "ERR_ADD_POST_SYS_BRANCHES";
        public const string ERR_EDIT_SYS_BRANCHES = "ERR_EDIT_SYS_BRANCHES";
        public const string ERR_EDIT_POST_SYS_BRANCHES = "ERR_EDIT_POST_SYS_BRANCHES";
        public const string ERR_DELETE_SYS_BRANCHES = "ERR_DELETE_SYS_BRANCHES";
        
        //SYSTEM REPORTING PERIODS
        public const string ERR_INDEX_SYS_REPORTING_PERIODS = "ERR_INDEX_SYS_REPORTING_PERIODS";
        public const string ERR_ADD_POST_SYS_REPORTING_PERIODS = "ERR_ADD_POST_SYS_REPORTING_PERIODS";
        public const string ERR_EDIT_SYS_REPORTING_PERIODS = "ERR_EDIT_SYS_REPORTING_PERIODS";
        public const string ERR_EDIT_POST_SYS_REPORTING_PERIODS = "ERR_EDIT_POST_SYS_REPORTING_PERIODS";
        public const string ERR_DELETE_SYS_REPORTING_PERIODS = "ERR_DELETE_SYS_REPORTING_PERIODS";
        public const string ERR_TO_DATE_LESS_THAN_FROM_DATE = "ERR_TO_DATE_LESS_THAN_FROM_DATE";
        
        //SYSTEM RIGHTS
        public const string ERR_INDEX_SYS_RIGHTS = "ERR_INDEX_SYS_RIGHTS";
        public const string ERR_ADD_POST_SYS_RIGHTS = "ERR_ADD_POST_SYS_RIGHTS";
        public const string ERR_EDIT_SYS_RIGHTS = "ERR_EDIT_SYS_RIGHTS";
        public const string ERR_EDIT_POST_SYS_RIGHTS = "ERR_EDIT_POST_SYS_RIGHTS";
        public const string ERR_DELETE_SYS_RIGHTS = "ERR_DELETE_SYS_RIGHTS";

        //SYSTEM USERS
        public const string ERR_INDEX_SYS_USERS = "ERR_INDEX_SYS_USERS";
        public const string ERR_ADD_POST_SYS_USERS = "ERR_ADD_POST_SYS_USERS";
        public const string ERR_EDIT_SYS_USERS = "ERR_EDIT_SYS_RIGHTS";
        public const string ERR_EDIT_POST_SYS_USERS = "ERR_EDIT_POST_SYS_USERS";
        public const string ERR_DELETE_SYS_USERS = "ERR_DELETE_SYS_USERS";

        //SYSTEM DECENTRALIZATION
        public const string ERR_INDEX_SYS_DECENTRALIZATION = "ERR_INDEX_SYS_DECENTRALIZATION";
        public const string ERR_ADD_POST_SYS_DECENTRALIZATION = "ERR_ADD_POST_SYS_DECENTRALIZATION";
        public const string ERR_EDIT_SYS_DECENTRALIZATION = "ERR_EDIT_SYS_DECENTRALIZATION";
        public const string ERR_EDIT_POST_SYS_DECENTRALIZATION = "ERR_EDIT_POST_SYS_DECENTRALIZATION";
        public const string ERR_DELETE_SYS_DECENTRALIZATION = "ERR_DELETE_SYS_DECENTRALIZATION";
        /************** END OF ERROR MESSAGES ****************/



        /************** SUCCESSFUL MESSAGES ****************/

        public const string SCC_ADD = "A new {0} has been added successfully";
        public const string SCC_EDIT_POST = "The {0} with ID {1} has been editted successfully";
        public const string SCC_DELETE = "A {0} has been deleted successfully";
        public const string SCC_UPDATE_FIPROPORTION = "Update financial proportion successfully";


        // FINANCIAL INDEX

        public const string SCC_ADD_FI_FINANCIAL_INDEX = "A new Financial Index has been added successfully";

        public const string SCC_EDIT_POST_FI_FINANCIAL_INDEX_1 = "The financial index with ID ";

        public const string SCC_EDIT_POST_FI_FINANCIAL_INDEX_2 = " has been editted successfully";

        public const string SCC_DELETE_FI_FINANCIAL_INDEX = "A financial index has been deleted successfully";


        // FINANCIAL INDEX LEVELS

        public const string SCC_ADD_FI_FINANCIAL_INDEX_LEVEL = "A new Financial Index Level has been added successfully";

        public const string SCC_EDIT_POST_FI_FINANCIAL_INDEX_LEVEL_1 = "The financial index level with ID ";

        public const string SCC_EDIT_POST_FI_FINANCIAL_INDEX_LEVEL_2 = " has been editted successfully";

        public const string SCC_DELETE_FI_FINANCIAL_INDEX_LEVEL = "A financial index level has been deleted successfully";




        //SYSTEM USER GROUPS
        public const string SCC_ADD_POST_SYS_USER_GROUPS = "SCC_ADD_POST_SYS_USER_GROUPS";
        public const string SCC_EDIT_POST_SYS_USER_GROUPS_1 = "SCC_EDIT_POST_SYS_USER_GROUPS_1";
        public const string SCC_EDIT_POST_SYS_USER_GROUPS_2 = "SCC_EDIT_POST_SYS_USER_GROUPS_2";
        public const string SCC_DELETE_SYS_USER_GROUPS = "SCC_DELETE_SYS_USER_GROUPS";
        
        //SYSTEM BRANCHES
        public const string SCC_ADD_POST_SYS_BRANCHES = "SCC_ADD_POST_SYS_BRANCHES";
        public const string SCC_EDIT_POST_SYS_BRANCHES_1 = "SCC_EDIT_POST_SYS_BRANCHES_1";
        public const string SCC_EDIT_POST_SYS_BRANCHES_2 = "SCC_EDIT_POST_SYS_BRANCHES_2";
        public const string SCC_DELETE_SYS_BRANCHES = "SCC_DELETE_SYS_BRANCHES";
        
        //SYSTEM REPORTING PERIODS
        public const string SCC_ADD_POST_SYS_REPORTING_PERIODS = "SCC_ADD_POST_SYS_REPORTING_PERIODS";
        public const string SCC_EDIT_POST_SYS_REPORTING_PERIODS_1 = "SCC_EDIT_POST_SYS_REPORTING_PERIODS_1";
        public const string SCC_EDIT_POST_SYS_REPORTING_PERIODS_2 = "SCC_EDIT_POST_SYS_REPORTING_PERIODS_2";
        public const string SCC_DELETE_SYS_REPORTING_PERIODS = "SCC_DELETE_SYS_REPORTING_PERIODS";
        
        //SYSTEM RIGHTS
        public const string SCC_ADD_POST_SYS_RIGHTS = "SCC_ADD_POST_SYS_RIGHTS";
        public const string SCC_EDIT_POST_SYS_RIGHTS_1 = "SCC_EDIT_POST_SYS_RIGHTS_1";
        public const string SCC_EDIT_POST_SYS_RIGHTS_2 = "SCC_EDIT_POST_SYS_RIGHTS_2";
        public const string SCC_DELETE_SYS_RIGHTS = "SCC_DELETE_SYS_RIGHTS";
        
        //SYSTEM USERS
        public const string SCC_ADD_POST_SYS_USERS = "SCC_ADD_POST_SYS_USERS";
        public const string SCC_EDIT_POST_SYS_USERS_1 = "SCC_EDIT_POST_SYS_USERS_1";
        public const string SCC_EDIT_POST_SYS_USERS_2 = "SCC_EDIT_POST_SYS_USERS_2";
        public const string SCC_DELETE_SYS_USERS = "SCC_DELETE_SYS_USERS";
        
        //SYSTEM DECENTRALIZATION
        public const string SCC_ADD_POST_SYS_DECENTRALIZATION = "SCC_ADD_POST_SYS_DECENTRALIZATION";
        public const string SCC_EDIT_SYS_DECENTRALIZATION = "SCC_EDIT_POST_SYS_DECENTRALIZATION_1";
        public const string SCC_EDIT_POST_SYS_DECENTRALIZATION = "SCC_EDIT_POST_SYS_DECENTRALIZATION_2";
        public const string SCC_DELETE_SYS_DECENTRALIZATION = "SCC_DELETE_SYS_DECENTRALIZATION";

        /************** END OF SUCCESSFUL MESSAGES ***************/
    }
}