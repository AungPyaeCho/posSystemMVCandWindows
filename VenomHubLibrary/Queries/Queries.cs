using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenomHubLibrary.Queries
{
    public class Queries
    {
        public static string CreateSale { get; } = @"
            INSERT INTO tblSale (invoiceNo, staffCode, staffName, memberCode, saleQty, totalAmount, receiveCash, refundCash, saleDate, paymentMethod, discount)
            VALUES (@invoiceNo, @staffCode, @staffName, @memberCode, @saleQty, @totalAmount, @receiveCash, @refundCash, @saleDate, @paymentMethod, @discount)";

        public static string CreateSaleDetail { get; } = @"
            INSERT INTO tblSaleDetail (invoiceNo, itemCode, saleQuantity, itemPrice, totalAmount, saleDate)
            VALUES (@invoiceNo, @itemCode, @saleQuantity, @itemPrice, @totalAmount, @saleDate)";

        public static string CreateLoginRecord { get; } = @"
            INSERT INTO tblLoginStaffDetail 
                (staffName, staffCode, staffRole, sessionId, sessionExpired, loginAt, logOutAt)
                VALUES 
                (@StaffName, @StaffCode, @StaffRole, @SessionId, @SessionExpired, @LoginAt, @LogOutAt)";

        public static string UpdateLoginRecord { get; } = @"
            UPDATE tblLoginStaffDetail
            SET  
                logOutAt = @LogOutAt
            WHERE
                staffCode = @staffCode";

        public static string UpdateItem { get; } = @"
            UPDATE tblItem
            SET itemRemainStock = itemRemainStock - @itemRemainStock, 
                itemSold = ISNULL(itemSold, 0) + @itemSold
            WHERE itemCode = @itemCode";

        public static string UpdateMember { get; } = @"
            UPDATE tblMember
            SET memberPoints = ISNULL(memberPoints, 0) + @memberPoints
            WHERE memberCode = @memberCode";

        public static string GetItems { get; } = @"
            SELECT 
                I.itemCode,
                I.itemName,
                I.itemSalePrice,
                I.itemWholeSalePrice,
                I.itemStock,
                I.itemRemainStock,
                I.itemSold,
                C.catName,
                B.brandName,
                SB.subBrandName,
                SC.subCatName
            FROM tblItem I
                INNER JOIN tblBrand B ON I.brandCode = B.brandCode
                INNER JOIN tblSubBrand SB ON I.subBrandCode = SB.subBrandCode
                INNER JOIN tblCategory C ON I.catCode = C.catCode
                INNER JOIN tblSubCategory SC ON I.subCatCode = SC.subCatCode";

        public static string GetItemByNameOrBarcode { get; } = @"
        SELECT DISTINCT
            I.itemCode,
            I.itemName,
            I.itemSalePrice,
            I.itemWholeSalePrice,
            I.itemStock,
            I.itemRemainStock,
            I.itemSold,
            C.catName,
            B.brandName,
            SB.subBrandName,
            SC.subCatName
        FROM tblItem I
        INNER JOIN tblBrand B ON I.brandCode = B.brandCode
        INNER JOIN tblSubBrand SB ON I.subBrandCode = SB.subBrandCode
        INNER JOIN tblCategory C ON I.catCode = C.catCode
        INNER JOIN tblSubCategory SC ON I.subCatCode = SC.subCatCode
        WHERE 
            (@itemName IS NULL OR I.itemName = @itemName) AND
            (@itemBarcode IS NULL OR I.itemBarcode = @itemBarcode)";

        public static string GetItemByNameOrBarcodeWithFilter { get; } = @"
        SELECT DISTINCT
            I.itemCode,
            I.itemName,
            I.itemSalePrice,
            I.itemWholeSalePrice,
            I.itemStock,
            I.itemRemainStock,
            I.itemSold,
            C.catName,
            B.brandName,
            SB.subBrandName,
            SC.subCatName
        FROM tblItem I
        INNER JOIN tblBrand B ON I.brandCode = B.brandCode
        INNER JOIN tblSubBrand SB ON I.subBrandCode = SB.subBrandCode
        INNER JOIN tblCategory C ON I.catCode = C.catCode
        INNER JOIN tblSubCategory SC ON I.subCatCode = SC.subCatCode
        WHERE 
            (@itemName IS NULL OR I.itemName = @itemName) AND
            (@itemBarcode IS NULL OR I.itemBarcode = @itemBarcode) AND
            (@categoryCode IS NULL OR I.catCode = @categoryCode) AND
            (@subCatCode IS NULL OR I.subCatCode = @subCatCode) AND
            (@brandCode IS NULL OR I.brandCode = @brandCode) AND
            (@subBrandCode IS NULL OR I.subBrandCode = @subBrandCode)";

        public static string GetItemByFilters { get; } = @"
        SELECT DISTINCT
            I.itemCode,
            I.itemName,
            I.itemSalePrice,
            I.itemWholeSalePrice,
            I.itemStock,
            I.itemRemainStock,
            I.itemSold,
            C.catName,
            B.brandName,
            SB.subBrandName,
            SC.subCatName
        FROM tblItem I
        INNER JOIN tblBrand B ON I.brandCode = B.brandCode
        INNER JOIN tblSubBrand SB ON I.subBrandCode = SB.subBrandCode
        INNER JOIN tblCategory C ON I.catCode = C.catCode
        INNER JOIN tblSubCategory SC ON I.subCatCode = SC.subCatCode
        WHERE 
            (@categoryCode IS NULL OR I.catCode = @categoryCode) AND
            (@subCatCode IS NULL OR I.subCatCode = @subCatCode) AND
            (@brandCode IS NULL OR I.brandCode = @brandCode) AND
            (@subBrandCode IS NULL OR I.subBrandCode = @subBrandCode)";

        public static string GetItemByAllSearch { get; } = @"
        SELECT DISTINCT
            I.itemCode,
            I.itemName,
            I.itemSalePrice,
            I.itemWholeSalePrice,
            I.itemStock,
            I.itemRemainStock,
            I.itemSold,
            C.catName,
            B.brandName,
            SB.subBrandName,
            SC.subCatName
        FROM tblItem I
        INNER JOIN tblBrand B ON I.brandCode = B.brandCode
        INNER JOIN tblSubBrand SB ON I.subBrandCode = SB.subBrandCode
        INNER JOIN tblCategory C ON I.catCode = C.catCode
        INNER JOIN tblSubCategory SC ON I.subCatCode = SC.subCatCode
        WHERE 
            (@itemName IS NULL OR I.itemName = @itemName) AND
            (@itemBarcode IS NULL OR I.itemBarcode = @itemBarcode) AND
            (@categoryCode IS NULL OR I.catCode = @categoryCode) AND
            (@subCatCode IS NULL OR I.subCatCode = @subCatCode) AND
            (@brandCode IS NULL OR I.brandCode = @brandCode) AND
            (@subBrandCode IS NULL OR I.subBrandCode = @subBrandCode)";

        public static string GetItemsByBarcode { get; } = @"
            SELECT DISTINCT
                I.itemCode,
                I.itemName,
                I.itemSalePrice,
                I.itemWholeSalePrice,
                I.itemStock,
                I.itemRemainStock,
                I.itemSold,
                C.catName,
                B.brandName,
                SB.subBrandName,
                SC.subCatName
            FROM tblItem I
                INNER JOIN tblBrand B ON I.brandCode = B.brandCode
                INNER JOIN tblSubBrand SB ON I.subBrandCode = SB.subBrandCode
                INNER JOIN tblCategory C ON I.catCode = C.catCode
                INNER JOIN tblSubCategory SC ON I.subCatCode = SC.subCatCode
            WHERE 
                (@itemBarcode IS NULL OR I.itemBarcode = @itemBarcode)";

        public static string GetItemByLike { get; } = @"
            SELECT DISTINCT
                I.itemCode,
                I.itemName,
                I.itemSalePrice,
                I.itemWholeSalePrice,
                I.itemStock,
                I.itemRemainStock,
                I.itemSold,
                C.catName,
                B.brandName,
                SB.subBrandName,
                SC.subCatName
            FROM tblItem I
                INNER JOIN tblBrand B ON I.brandCode = B.brandCode
                INNER JOIN tblSubBrand SB ON I.subBrandCode = SB.subBrandCode
                INNER JOIN tblCategory C ON I.catCode = C.catCode
                INNER JOIN tblSubCategory SC ON I.subCatCode = SC.subCatCode
            WHERE 
                (@itemName IS NULL OR I.itemName LIKE '%' + @itemName + '%') AND
                (@itemBarcode IS NULL OR I.itemBarcode LIKE '%' + @itemBarcode + '%') AND
                (@categoryCode IS NULL OR I.catCode = @categoryCode) AND
                (@subCatCode IS NULL OR I.subCatCode = @subCatCode) AND
                (@brandCode IS NULL OR I.brandCode = @brandCode) AND
                (@subBrandCode IS NULL OR I.subBrandCode = @subBrandCode)";

        public static string BuildDynamicLikeQuery(dynamic parameters)
        {
            var baseQuery = @"
                SELECT 
                    I.itemCode,
                    I.itemName,
                    I.itemSalePrice,
                    I.itemWholeSalePrice,
                    I.itemStock,
                    I.itemRemainStock,
                    I.itemSold,
                    C.catName,
                    B.brandName,
                    SB.subBrandName,
                    SC.subCatName
                FROM tblItem I
                    INNER JOIN tblBrand B ON I.brandCode = B.brandCode
                    INNER JOIN tblSubBrand SB ON I.subBrandCode = SB.subBrandCode
                    INNER JOIN tblCategory C ON I.catCode = C.catCode
                    INNER JOIN tblSubCategory SC ON I.subCatCode = SC.subCatCode
                WHERE 1=1";

            var conditions = new List<string>();

            if (parameters.itemName != null)
            {
                conditions.Add("I.itemName LIKE '%' + @itemName + '%'");
            }
            if (parameters.itemBarcode != null)
            {
                conditions.Add("I.itemBarcode LIKE '%' + @itemBarcode + '%'");
            }
            if (parameters.catCode != null)
            {
                conditions.Add("I.catCode = @catCode");
            }
            if (parameters.subCatCode != null)
            {
                conditions.Add("I.subCatCode = @subCatCode");
            }
            if (parameters.brandCode != null)
            {
                conditions.Add("I.brandCode = @brandCode");
            }
            if (parameters.subBrandCode != null)
            {
                conditions.Add("I.subBrandCode = @subBrandCode");
            }

            if (conditions.Count > 0)
            {
                baseQuery += " AND " + string.Join(" AND ", conditions);
            }

            return baseQuery;
        }

        public static string GetUser { get; } = @"
            SELECT
                staffCode,
                staffName,
                staffEmail,
                staffPassword,
                staffRole
            FROM tblStaff
            WHERE
                staffName = @staffName AND
                staffPassword = @staffPassword";


        public static string GetCategories { get; } = @"
            SELECT catCode, catName
            FROM tblCategory";

        public static string GetSubCategories { get; } = @"
            SELECT subCatCode, subCatName
            FROM tblSubCategory";

        public static string GetBrands { get; } = @"
            SELECT brandCode, brandName
            FROM tblBrand";

        public static string GetSubBrands { get; } = @"
            SELECT subBrandCode, subBrandName
            FROM tblSubBrand";

        public static string GetPromotions { get; } = @"
            SELECT proName, proCode
            FROM tblPromotion";

        public static string GetDiscounts { get; } = @"
            SELECT disValue, disName
            FROM tblDiscount";




    }

    //public class SearchParameters
    //{
    //    public string itemName { get; set; }
    //    public string itemBarcode { get; set; }
    //    public string categoryCode { get; set; }
    //    public string subCatCode { get; set; }
    //    public string brandCode { get; set; }
    //    public string subBrandCode { get; set; }
    //}
}
