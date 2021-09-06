from bs4 import BeautifulSoup 
import requests
import json
import csv
import time
import os

headers = {
    "accept": "image/avif,image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8",
    "user-agent" : "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.131 Safari/537.36"
}

url = "https://sushishop.ru"

def get_all_pages():
    res = requests.get(url=url, headers=headers)
    
    if not os.path.exists("data"):
        os.mkdir("data")

    with open("index.html", "w", encoding="utf-8") as file:
    	file.write(res.text)

    with open("index.html", "r", encoding="utf-8") as file:
        src = file.read()

    soup = BeautifulSoup(src, "lxml")
    cards_a = [a["href"] for a in soup.find("div", class_="home-app-menu__links").find_all("a")]
   
    cards_count = len(cards_a)

    for i in range(0, cards_count):
        new_url = url + cards_a[i]
        
        r = requests.get(url=new_url, headers=headers)

        if not os.path.exists("data" + cards_a[i]):
            os.mkdir("data" + cards_a[i])

        with open("data" + cards_a[i] + f"/page_{i}.html", "w", encoding="utf-8") as file:
            file.write(r.text)

        time.sleep(2)

    return cards_count, cards_a

def collect_data(pages_count, pages_href):
    for page in range(0, pages_count):
        with open("data" + pages_href[page] + f"/page_{page}.csv", "w", newline='') as file:
            writer = csv.writer(file, delimiter=';')
        
            writer.writerow(
                (
                    "Название",
                    "Состав",
                    "Цена"
                )
            )
        
        data = []
        
        new_url = url + pages_href[page]
        
        r = requests.get(url=new_url, headers=headers)
        
        soup = BeautifulSoup(r.text, "lxml")
        
        items_cards = soup.find("div", class_="category__items").find_all("a")
        
        for item in items_cards:
            product_name = item.find("span", class_="category__item-title").text.strip()
            product_structure = item.find("span", class_="category__item-description").text.strip()        
            product_price = item.find("span", class_="category__item-price").text.replace("\u20bd", "").split(" ")
            if len(product_price) > 2:
                product_price = product_price[1]
            else:
                product_price = product_price[0]
        
            data.append(
                {
                    "product_name": product_name,
                    "product_structure": product_structure,
                    "product_price": product_price
                }
            )
        
            with open("data" + pages_href[page] + f"/page_{page}.csv", "a", newline='') as file:
                writer = csv.writer(file, delimiter=';')
        
                writer.writerow(
                    (
                        product_name,
                        product_structure,
                        product_price
                    )
                )
        
        print(f"[INFO] Обработана страница {page + 1}/{pages_count}")
        time.sleep(2)
        
        with open("data" + pages_href[page] + f"/page_{page}.json", "a") as file:
            json.dump(data, file, indent=4, ensure_ascii=False)


def main():
    pages_count, pages_href = get_all_pages()
    collect_data(pages_count=pages_count, pages_href=pages_href)


if __name__ == "__main__":
    main()
