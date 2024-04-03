// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function customMarkdownParser(plainText) {
    // Reemplaza los caracteres especiales del markdown para su representación HTML
    var html = plainText
        // Encabezados
        .replace(/^#\s+(.*)$/gm, '<h1>$1</h1>')
        .replace(/^##\s+(.*)$/gm, '<h2>$1</h2>')
        .replace(/^###\s+(.*)$/gm, '<h3>$1</h3>')
        .replace(/^####\s+(.*)$/gm, '<h4>$1</h4>')
        .replace(/^#####\s+(.*)$/gm, '<h5>$1</h5>')
        .replace(/^######\s+(.*)$/gm, '<h6>$1</h6>')
        // Negrita
        .replace(/\*\*(.*)\*\*/g, '<strong>$1</strong>')
        // Itálica
        .replace(/\*(.*)\*/g, '<em>$1</em>')
        // Enlaces
        .replace(/\[([^\]]+)\]\(([^)]+)\)/g, '<a href="$2">$1</a>')
        // Líneas horizontales
        .replace(/^\s*([-*]\s*){3,}\s*$/gm, '<hr>')
        // Líneas de código
        .replace(/```(.*)\n([\s\S]*?)\n```/g, '<pre><code>$2</code></pre>')
        // Listas ordenadas
        .replace(/^\d+\.\s.*$/gm, function(match) {
            var items = match.split('.').slice(1);
            var htmlList = items.map(function(item) {
                return '<li>' + item.trim() + '</li>';
            }).join('');
            return '<ol>' + htmlList + '</ol>';
        })
        // Listas sin orden
        .replace(/^\*\s.*$/gm, function(match) {
            var items = match.split('*').slice(1);
            var htmlList = items.map(function(item) {
                return '<li>' + item.trim() + '</li>';
            }).join('');
            return '<ul>' + htmlList + '</ul>';
        });

    return html;
}
