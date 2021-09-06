from bs4 import BeautifulSoup 
import requests
import json
import csv
import time

headers = {
    "accept": "*/*",
    "user-agent" : "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.131 YaBrowser/21.8.1.468 Yowser/2.5 Safari/537.36"
}

url = "https://pizzaluga.ru/picca"

def get_all_pages():
    res = requests.get(url=url, headers=headers)
    src = res.text

    #with open("index.html", "w", encoding="utf-8") as file:
    #	file.write(src)
    
    soup = BeautifulSoup(src, 'lxml')
    
    pages_count = int(soup.find("ul", class_="custom-pagination visible").find_all("a")[-2].text)
    #print(pages_count)
    
    return pages_count + 1


def collect_data(pages_count):
    data = []

    with open("data_pizza.csv", "w", newline='') as file:
        writer = csv.writer(file, delimiter=';')
    
        writer.writerow(
            (
                "Название",
                "Состав",
                "Цена"
            )
        )

    for i in range(1, pages_count):
        url = f"https://pizzaluga.ru/picca/page-{i}"
    
        r = requests.get(url=url, headers=headers)
    
        soup = BeautifulSoup(r.text, "lxml")
    
        items_cards = soup.find_all("div", class_="item-view-product")
    
        for item in items_cards:
            product_name = item.find("div", class_="product-preview").find("div", class_="info").find("h2").text.strip()
            product_structure = item.find("div", class_="product-preview").find("div", class_="info").find("div", class_="text").text.strip()        
            product_price = item.find("div", class_="product-preview").find("div", class_="info").find("div", id="list-product").find("div", id="price-preview").text.strip()
    
            data.append(
                {
                    "product_name": product_name,
                    "product_structure": product_structure,
                    "product_price": product_price
                }
            )
    
            with open("data_pizza.csv", "a", newline='') as file:
                writer = csv.writer(file, delimiter=';')
    
                writer.writerow(
                    (
                        product_name,
                        product_structure,
                        product_price
                    )
                )
    
        print(f"[INFO] Обработана страница {i}/2")
        time.sleep(2)
    
    with open("data_pizza.json", "a") as file:
        json.dump(data, file, indent=4, ensure_ascii=False)


def main():
    pages_count = get_all_pages()
    collect_data(pages_count=pages_count)


if __name__ == "__main__":
    main()