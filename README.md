資料庫轉檔驗證小工具說明
=============
for specific project only.
謹留存用。並移除敏感資訊。

## 1.移轉需求/規格
* from：Oracle8i - 8.1.7.4.1
* to：SQL Server 2008R2
* 目標為讓VB6可後續改版

## 2.移轉DB與帳密
來源DB為Oracle8i，共7個DB又分有２個SID。

## 3.開發環境
* Oracle ODAC 10.2.0.2.21
* SOL Server 2008 R2
* Visual Studio 2008 SP1 with BI module
* Toad 7.4.0.3
* SSMA 5.2

## 4.交接項目放置位置
* 資料庫位置：
* 資料庫密碼：
* 資料庫備份檔：E:\MigrationDB_????.7z
* SSMA專案檔：E:\MigrationDB\SSMAProject\
* SSMA專案備份：E:\MigrationDB\SSMAProject_????.7z\
* 驗證小工具NNOraToSqlValidator2
* VS2008專案檔：E:\VS2008Project\NNOraToSqlValidator2\
 * 發行位置：⋯\_issue\
 * 驗證小工具資料庫：⋯\Database1.sd
* 放置連線資訊與驗證紀錄。可用「SQL Server Management Studio」開啟。其中Table功能說明如下：
 * ConnInfo：連線資訊。
 * TabRowCnt：筆數與比較紀錄。
 * TabCompareRecord：欄位內容比對紀錄。
 * TabHardCmpRecord：保留未使用。

## 5.其他說明與注意事項
* 共有7+1個DB要部署。
目標有7個資料DB要移轉，另外SSMA輔助資料庫sysdb也須一併移轉。所以共8個。
* 改用SSMA移轉Data。以資料庫為單位。無順序但Index必須先停止不然可能會跑五天。
* 建議移轉Data前置動作：
 * Truncate Table → SP：u_TruncateAllTables
 * Disable Index → SP：u_LetAllIndexes 'DISABLE';
* 後置動作再
 * Rebuild Index → u_LetAllIndex 'REBUILD';
* SequenceEmu使用方法：
 * 因SQL Server 2008無Sequence物件故模擬之。
 * CreateSequenceEmu
 * NextValueFor
* 注意：ROWID議題，移轉時自動加入。否則trigger會無效
* 注意：對SQL Server的語法必須全大寫。因已設定大小寫視為相異。

## 6.轉檔與驗證操作註記
* 以DB為單位處理。
* 用SSMA搬資料。
 * menu［File\Open Projecr］開啟專案。click［Oracle Metadata Explorer\{SID, ORA1}\Schemas\{DB_Name}\Tables］，popup menu ［Migrate Data］開始移轉Data。
* 驗證操作說明：取筆數與比較,也是以DB為單位。
* 首先menu［Test connection］試看連缐設定是否正確。若有修改請［Save］。
* 再menu［Count table rows and compare］取筆數與比較。
 * 依［Select DB］選取目標DB，然後［List Table］，然後［Count Rows and Compare］。最後請［Save］。
* 驗證操作說明part2：一一比對欄位內容
* menu［Compare fields of one table］。
 * 於［DB-Table］點選｛DB Name｝再點選第一個｛Table｝，按下［Batch Compare］將依Table一個一個往下比較直到遇到下一個DB為止。
* 也可按下［Compare］或快捷鍵＜Ctrl＋C＞只比此Table。
