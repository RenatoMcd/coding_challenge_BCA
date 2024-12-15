Code explanation

-   Opted to create a base **Vehicle** class that encapsulates all the
    > shared attributes.

-   All the other specific vehicle types (**Hatchback**, **Sudan**,
    > **SUV**, **Truck**) inherit from the base class and define their
    > unique attributes. This allows for polymorphic handling of
    > vehicles.

-   Opted to create a separate class to manage the auctions
    > (**Auction**). This ensures that the single responsibility
    > principle is applied and makes it easier to read, test, and scale
    > the code in the future.

-   Opted to create a **CarAuctionManagementSystem** class that has a
    > car and auction inventory so that it was possible to add and
    > search vehicles and to start, close and bid in an auction.

-   In the **SearchVehicle** method it was assumed that the user was not
    > forced to pass all the 4 search criteria.

-   In the bidder argument it was assumed that there was no need to
    > create a user class and mock users.
