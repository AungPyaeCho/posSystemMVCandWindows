using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenomHubLibrary.Queries
{
    public class Queries
    {
        public static string Create { get; set; } = @"";

        public static string GetItems { get; } = @"
            SELECT 
                I.itemCode,
                I.itemName,
                I.itemSalePrice,
                I.itemWholeSalePrice,
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
            SELECT 
                I.itemCode,
                I.itemName,
                I.itemSalePrice,
                I.itemWholeSalePrice,
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

        public static string GetItemByNameOrBarcodeUnKown { get; } = @"
            SELECT 
                I.itemCode,
                I.itemName,
                I.itemSalePrice,
                I.itemWholeSalePrice,
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


    }
}
