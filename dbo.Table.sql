CREATE TABLE [dbo].[Table] (
    [Id] INT NOT NULL IDENTITY(1,1), -- Auto-incrementing Id for each round
    [RoundNumber] INT NOT NULL, -- Round number
    [PlayerCards] VARCHAR(100) NOT NULL, -- Player's cards in hand
    [DealerCards] VARCHAR(100) NOT NULL, -- Dealer's cards in hand
    [PlayerTotal] INT NOT NULL, -- Total value of player's cards
    [DealerTotal] INT NOT NULL, -- Total value of dealer's cards
    [Result] VARCHAR(10) NOT NULL, -- Win/Tie/Lose result
    PRIMARY KEY CLUSTERED ([Id] ASC) -- Primary key on Id
);
