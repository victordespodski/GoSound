"""Real HTTP smoke checks; requires a running app with a migrated SQL database."""
from urllib.request import urlopen
from urllib.error import HTTPError, URLError
from pathlib import Path
import time
from html import unescape
base='http://127.0.0.1:5180'
for attempt in range(30):
    try:
        with urlopen(base, timeout=3) as r:
            if r.status==200: break
    except (OSError, URLError): time.sleep(1)
else: raise SystemExit('Application did not start.')
results=[]
def check(path, expected_status, expected_text):
    try:
        with urlopen(base+path,timeout=20) as r: status,html=r.status,r.read().decode()
    except HTTPError as e: status,html=e.code,e.read().decode()
    assert status==expected_status,(path,status)
    assert expected_text in unescape(html),(path,'missing expected text')
    results.append(f'PASS {path} -> {status}')
    Path('verification-results.txt').write_text('\n'.join(results)+'\n')
check('/',200,'GoSound')
check('/Categories',200,'Технологии')
check('/Podcasts',200,'Все още няма публикувани предавания.')
check('/Podcasts?categoryId=1',200,'Все още няма публикувани предавания.')
check('/Podcasts/Details/999999',404,'Тази страница не е достъпна.')
check('/missing-page',404,'Тази страница не е достъпна.')
print('\n'.join(results))
