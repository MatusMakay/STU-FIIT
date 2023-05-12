# Co este treba spravit

-   na home page akcie pridat do kosika nefunguju => alex done
-   opravit pridavanie do kosika z produktu => alex done
-   usporiadanie podla ceny dorobit => matus
-   filtrovanie treba spravit => matus done
-   platba => matus + alex
    -   nedovolit prejst na platbu s prazdnym kosikom => alex
    -   vybrat len jednu z moznosti dopravy a sposobu platby => matus
    -   predvolena adresa ak je pouzivatel prihlaseny => alex
    -   nedovolenie pokracovat ak nezada adresu => matus
-   zhrnutie objednavky treba => alex
-   na mobonej verzii namiesto ceny delete => done :kissing:

-   a admin zona (mozno frontend) + servovanie => matus + alex

ak budeme stihat

-   zobrazit dostupna velkost a farba
-   fix pridanie do kosika vypis

# Co este treba spravit

-   platba => matus + alex
    -   nedovolit prejst na platbu s prazdnym kosikom => alex
    -   vybrat len jednu z moznosti dopravy a sposobu platby => matus done
    -   nedovolenie pokracovat ak nezada adresu => matus done

# TODO fast

-   [x] dorobit doplnenie atributov do sumaru objednavky
-   [ ] admin zona
-   [ ] pri deletovani itemu z kosika vrat naspat celu stranku

## SQL on creating products

-   only for man category

INSERT INTO products (id, item_name, product_img, description, product_price, category, type, brand, color, size, number_products, created_at, updated_at)
VALUES
(19, 'Tricko s dlhym rukavom Jedinecne', 'productImg1.jpg', 'Nieco skvele', 29.99, 'Man', 'LongSleeve', 'Adidas', 'White', 'S', 10, NOW(), NOW()),
(20, 'Tricko s dlhym rukavom Jedinecne', 'productImg2.jpg', 'Naj tovar', 30, 'Man', 'ShortSleeve', 'Humanic', 'Black', 'M', 10, NOW(), NOW()),
(21, 'Tricko s dlhym rukavom Jedinecne', 'productImg3.jpg', 'To chces', 41, 'Man', 'Hoddies', 'Supreme', 'Pink', 'L', 10, NOW(), NOW()),
(22, 'Tricko s dlhym rukavom Jedinecne', 'productImg4.jpg', 'Fakt moc', 20, 'Man', 'Jeans', 'Nike', 'White', 'XL', 10, NOW(), NOW()),
(23, 'Tricko s dlhym rukavom Jedinecne', 'productImg5.jpg', 'A mozno trocha viac', 10, 'Man', 'ShortJeans', 'Adidas', 'Black', 'XLL', 10, NOW(), NOW()),
(24, 'Tricko s dlhym rukavom Jedinecne', 'productImg6.jpg', 'A este trosku', 11, 'Man', 'Jeans', 'Humanic', 'Pink', 'S', 10, NOW(), NOW()),
(25, 'Tricko s dlhym rukavom Jedinecne', 'productImg7.jpg', 'Ved vies ako to chodi', 12, 'Man', 'Hoddies', 'Adidas', 'White', 'M', 10, NOW(), NOW()),
(26, 'Tricko s dlhym rukavom Jedinecne', 'productImg8.jpg', 'Uz nevladzem', 13, 'Man', 'ShortJeans', 'Nike', 'Black', 'XL', 10, NOW(), NOW()),
(27, 'Tricko s dlhym rukavom Jedinecne', 'productImg9.jpg', 'Naco fakt', 14, 'Man', 'Jeans', 'Humanic', 'Pink', 'L', 10, NOW(), NOW()),
(28, 'Tricko s dlhym rukavom Jedinecne', 'productImg10.jpg', 'Fic fic fic', 15, 'Man', 'LongSleeve', 'Supreme', 'White', 'XLL', 10, NOW(), NOW());
