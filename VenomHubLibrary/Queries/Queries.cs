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

        public static string GetItemBy { get; } = @"
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
                (@catCode IS NULL OR I.catCode = @catCode) AND
                (@subCatCode IS NULL OR I.subCatCode = @subCatCode) AND
                (@brandCode IS NULL OR I.brandCode = @brandCode) AND 
                (@brandCode IS NULL OR I.brandCode = @brandCode)";

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



        public static string GetItemByNameOrBarcodeUnKown { get; } = @"
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
            WHERE 
                (@itemName IS NULL OR I.itemName LIKE '%' + @itemName + '%') AND
                (@itemBarcode IS NULL OR I.itemBarcode LIKE '%' + @itemBarcode + '%')";


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
}
