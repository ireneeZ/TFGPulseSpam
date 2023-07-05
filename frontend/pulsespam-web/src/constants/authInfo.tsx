const user = localStorage.getItem('user');
let t = JSON.parse(user || '{}');

export const token = t;