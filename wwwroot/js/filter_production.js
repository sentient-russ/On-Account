function currencyToFloat(e) { let t = e.replace(/[^0-9.]/g, ""); return parseFloat(t) } document.addEventListener("DOMContentLoaded", (function () { const e = document.getElementById("getDateRangeButton"), t = document.getElementById("startDateDropdownForm"), n = document.getElementById("endDateDropdownForm"); var o = document.querySelectorAll("#tableBody tr"); e.addEventListener("click", (function (e) { e.preventDefault(); const a = new Date(t.value), l = new Date(n.value), r = []; o.forEach((e => { e.style.display = ""; let t = e.querySelector("td#transaction-date").textContent.trim(); const [n, o, d] = t.split("/").map(Number), c = new Date(d, n - 1, o); if (c >= a && c <= l) { const t = e.getAttribute("data-journal-id"); t && (r.push(t), e.style.display = "") } else { const t = e.getAttribute("data-journal-id"); r.includes(t) || (e.style.display = "none") } })) })) })), document.addEventListener("DOMContentLoaded", (function () { const e = document.getElementById("getAmountRangeButton"), t = document.getElementById("startAmountDropdownForm"), n = document.getElementById("endAmountDropdownForm"); var o = document.querySelectorAll("#tableBody tr"); e.addEventListener("click", (function (e) { e.preventDefault(); const a = currencyToFloat(t.value), l = currencyToFloat(n.value); var r = []; Array.from(o).reverse().forEach((e => { e.style.display = "none"; const t = e.getAttribute("data-journal-id"); if (r.includes(t)) e.style.display = ""; else { const t = e.querySelector("td#transaction-amount"); if (t) { let n = t.textContent.trim().replace(/[^0-9.]/g, ""); const o = parseFloat(n); if (o >= a && o <= l) { console.log(o); const t = e.getAttribute("data-journal-id"); console.log(t), t && (r.push(t), e.style.display = "") } else e.style.display = "none" } } })) })) })), document.addEventListener("DOMContentLoaded", (function () { const e = document.getElementById("searchInput"), t = document.getElementById("tableBody"), n = document.querySelectorAll("table tbody tr"); t.getElementsByTagName("tr"), e.value.trim(); "" !== e.value.trim() && function (e) { setTimeout((() => { n.forEach((t => { const n = t.querySelector("td.text-center[hidden]").innerText.trim(); new RegExp(`\\b${e}\\b`).test(n) ? t.style.display = "" : t.style.display = "none" })) }), 200) }(e.value.trim().toUpperCase()) }));