Feature: CodilityTest
	In order to verify the wishlist

    Background: 
        Given I launch "DemoShop" portal web
		And I mouseover on Categories to select an option

	Scenario: SC001: Add different products to wishlist
		When I click on "Clothing" from Categories menu
		Then I should see the title to be "Clothing"
			And Verify the "Clothing" section is displyed
		When I Click on Add to Cart button for 
			|Black trousers|
			|Single Shirt|
			|Hard top|
			|Bikini|
		And I Click on "Cart" icon
		Then I should see the "4" products are listed.
	
	Scenario: SC002: Add the lowest price item to cart
		When I Click on "Watches" from the Categories menu
		Then I should see the title to be "Watches"
			And Verify the "Watches" section is displyed
		When I Click on sort option to choose "Sort by price: low to high"
			And I Click on "first" product from the section
			And I Click on "Cart" icon
		Then I should see the products is listed
