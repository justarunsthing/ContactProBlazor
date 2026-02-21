export function initToast(toastElement) {
    const toast = bootstrap.Toast.getOrCreateInstance(toastElement);
    toast.show();
}