CREATE PROCEDURE SalespecAdd
    @Headid INT,
    @Nomenid INT,
    @Quant INT,
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @quantp INT,
            @prid INT,
            @store INT;

    BEGIN TRY

        -- получаем склад
        SELECT @store = storeid
        FROM Salehead
        WHERE id = @Headid;

        IF @store IS NULL
            THROW 50001, 'Ќакладна€ не найдена', 1;

        -- проверка товара
        IF NOT EXISTS (SELECT 1 FROM Nomenlist WHERE id = @Nomenid)
            THROW 50002, '“акой товар не найден', 1;

        -- получаем остатки
        SELECT @prid = id,
               @quant = quanttosale
        FROM Pricelist
        WHERE nomenid = @Nomenid
          AND storeid = @store;

        IF @prid IS NULL
            THROW 50004, '“овар не найден на складе', 1;

        IF @quant < @Quant
            THROW 50003, ' оличество недостаточно дл€ продажи', 1;

        BEGIN TRANSACTION;

            UPDATE Pricelist
            SET quanttosale = quanttosale - @Quant,
                quantreserve = quantreserve + @Quant
            WHERE id = @prid;

            INSERT INTO Salespec(headid, nomenid, quant)
            VALUES(@Headid, @Nomenid, @Quant);

            SET @Id = SCOPE_IDENTITY();

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END