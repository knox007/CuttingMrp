
--- �˽ű��������������ݿ� CuttingMrp ��
--- �������޸����е�CuttingMrpΪ�������ݿ�

USE[master]
    GO
    ALTER DATABASE CuttingMrp SET RECOVERY SIMPLE WITH NO_WAIT 
    GO
    ALTER DATABASE CuttingMrp SET RECOVERY SIMPLE   --��ģʽ
    GO
    USE CuttingMrp 
    GO
    DBCC SHRINKFILE (N'CuttingMrp_Log' , 11, TRUNCATEONLY)
    GO
    
    USE[master]
    GO

    ALTER DATABASE CuttingMrp SET RECOVERY  FULL WITH NO_WAIT

    GO

    ALTER DATABASE CuttingMrp SET RECOVERY FULL  --��ԭΪ��ȫģʽ

    GO