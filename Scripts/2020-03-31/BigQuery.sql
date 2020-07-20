SELECT  distinct  tblParts.PartID,tblParts.PartCode,tblParts.PartCodeOther,tblParts.PartDesc,PG.GroupName,tblParts.LastCost,tblParts.AVGCost,cast('false' as bit) Checking 
,  ((select ((isnull(AllQ.x1,0) + ISNULL( ALLQ.x2,0) + isnull(ALLQ.x5,0)) - (isnull(ALLQ.x3,0) + isnull(ALLQ.x4,0))) as OnHand 
from  (select (select isnull(sum(Qauntity),0) from tblStockParts  
where tblStockParts.PartID = tblParts.PartID and tblStockParts.StockID = 3 
group by tblStockParts.PartID) as x1, 
(select isnull(sum(Qantity),0) from tblIncomeDetails  
left outer join tblIncome on tblIncomeDetails.IncomeID = tblIncome.ID  where tblIncomeDetails.PartID = tblParts.PartID  
and tblIncome.StockID = 3 group by tblIncomeDetails.PartID ) as x2, 
(select isnull(sum(Qantity),0) from tblOutcomeDetails  
left outer join tblOutcome on tblOutcomeDetails .OutcomeID =tblOutcome.ID  where(tblOutcomeDetails.PartID = tblParts.PartID And tblOutcome.StockID = 3) 
group by tblOutcomeDetails .PartID ) as x3, 
(select  isnull(sum(Quantity),0) from tblRetrieve  
left outer join tblRetrieve_Parts  on tblRetrieve.RetrieveID =tblRetrieve_Parts.RetrieveID  
where(tblRetrieve_Parts.PartID = tblParts.PartID And tblRetrieve.StockID = 3)  
group by tblRetrieve_Parts.PartID ) as x4,  
(select isnull(sum(Quantity),0) from tblRetrieveWo  
left outer join tblRetrieveWo_Parts  on  tblRetrieveWo.RetrieveID  =tblRetrieveWo_Parts.RetrieveID  
where(tblRetrieveWo_Parts.PartID = tblParts.PartID And tblRetrieveWo.StockID = 3)  
group by tblRetrieveWo_Parts.PartID) as x5)as ALLQ))as OnHand,  
tblParts.Minimum, tblParts.LocationID, tblParts.Measure, tblParts.VendorID, tblParts.Keyword, tblParts.EquipmentID, tblParts.STDOrder, 
tblParts.PartNote, tblParts.OnOrder, tblParts.ShelfID,  tblParts.Commited, tblParts.LeadTime, tblParts.Warranty,
tblParts.Inactive, tblParts.Review,tblParts.CurrancyTypeID, ISNULL(tblLocations.LocationCode, '') AS LocationCode, 
ISNULL(tblVendors.VendorID, '') AS VendorCode, ISNULL(tblEquipments.EquipmentID, '') AS EquipmentCode ,ISNULL(tblShelves.ID, '') AS ShelfName, 
tblCurrency.CurrencyName  
FROM tblParts 
LEFT OUTER JOIN tblpartgroupdetails PGD ON  tblParts.PartID = PGD.PartID 
LEFT JOIN tblpartgroup PG ON PG.PartGroupID = PGD.PartGroupID  
LEFT JOIN  tblCurrency ON tblParts.CurrancyTypeID = tblCurrency.CurrencyID 
LEFT OUTER JOIN  tblLocations ON tblParts.LocationID = tblLocations.LocationID  
LEFT OUTER JOIN  tblVendors ON tblParts.VendorID = tblVendors.Serial  
LEFT OUTER JOIN  tblEquipments ON tblParts.EquipmentID = tblEquipments.Serial 
LEFT OUTER JOIN  tblShelves ON tblParts.ShelfID = tblShelves.ID  order by tblparts.PartID 